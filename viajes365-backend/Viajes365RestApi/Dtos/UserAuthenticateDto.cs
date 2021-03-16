using System.ComponentModel.DataAnnotations;

namespace Viajes365RestApi.Dtos

{
    public class UserAuthenticateDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}