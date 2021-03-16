using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Viajes365RestApi.Dtos;
using Viajes365RestApi.Entities;
using Viajes365RestApi.Filters;
using Viajes365RestApi.Helpers;
using Viajes365RestApi.Services;
using Viajes365RestApi.Wrappers;

namespace Viajes365RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly DataContext _context;

        public RolesController(DataContext context, IMapper mapper, IUriService uriService)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles([FromQuery] PaginationFilter filter)
        {
            List<RoleDto> roles = new List<RoleDto>();
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var totalElements = await _context.Roles.CountAsync();

            if (totalElements == 0)
            {

                return NotFound(new PagedResponse<List<RoleDto>>() { Message = "NO HAY RESULTADOS CON LOS PARAMETROS INDICADOS", ErrorCode = 416 });

            }
            else
            {
                var result = await _context.Roles
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
                result.ForEach(u => roles.Add(_mapper.Map<RoleDto>(u)));
                PagedResponse<List<RoleDto>> pagedResponse = Pagination.CreatePagedReponse<RoleDto>(roles, validFilter, totalElements, _uriService, route);
                return Ok(pagedResponse);
            }
        }
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(long id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound(new Response<RoleDto>() { Message = "ROL NO ENCONTRADO", ErrorCode = 416 });
            }

            RoleDto model = _mapper.Map<RoleDto>(role);
            return Ok(new Response<RoleDto>(model));
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(long id, Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(long id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
