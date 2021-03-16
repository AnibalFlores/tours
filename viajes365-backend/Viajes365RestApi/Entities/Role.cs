using System.ComponentModel.DataAnnotations;

namespace Viajes365RestApi.Entities
{
    public class Role : Base
    {
        [Key]
        public long RoleId { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
