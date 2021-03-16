using Viajes365RestApi.Entities;

namespace Viajes365RestApi.Dtos
{
    public class UserDto : Base
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public virtual RoleDto Role { get; set; }
    }
}