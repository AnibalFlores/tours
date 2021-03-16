using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Viajes365RestApi.Dtos;
using Viajes365RestApi.Entities;
using Viajes365RestApi.Filters;
using Viajes365RestApi.Handlers;
using Viajes365RestApi.Helpers;
using Viajes365RestApi.Services;
using Viajes365RestApi.Wrappers;

namespace Viajes365RestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly AppSettings _appSettings;
        private readonly DataContext _context;

        public UsersController(DataContext context,
            IUserService userService,
            IMapper mapper,
            IUriService uriService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _appSettings = appSettings.Value;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthenticateDto model)
        {
            var user = _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim("Role", user.Role.RoleName)
                }),
                // TODO make expiration time a Param
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                UserId = user.UserId,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                return await _userService.Create(user, model.Password, model.RoleId);

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] PaginationFilter filter)
        {
            List<UserDto> users = new List<UserDto>();
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var totalElements = await _context.Users.CountAsync();

            if (totalElements == 0)
            {

                return NotFound(new PagedResponse<List<UserDto>>() { Message = "NO HAY RESULTADOS CON LOS PARAMETROS INDICADOS", ErrorCode = 416 });

            }
            else
            {
                var result = await _context.Users
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .Include(u => u.Role)
            .ToListAsync();
                result.ForEach(u => users.Add(_mapper.Map<UserDto>(u)));
                PagedResponse<List<UserDto>> pagedResponse = Pagination.CreatePagedReponse<UserDto>(users, validFilter, totalElements, _uriService, route);
                return Ok(pagedResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new Response<UserDto>() { Message = "USUARIO NO ENCONTRADO", ErrorCode = 416 });
            }

            UserDto model = _mapper.Map<UserDto>(user);
            return Ok(new Response<UserDto>(model));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateDto model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.UserId = id;

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
