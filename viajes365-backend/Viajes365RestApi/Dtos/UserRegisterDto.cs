using System.ComponentModel.DataAnnotations;

namespace Viajes365RestApi.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public long RoleId { get; set; }
    }
}