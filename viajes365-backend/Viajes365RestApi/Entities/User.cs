using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace Viajes365RestApi.Entities
{
    public class User : Base
    {

        [Key]
        public long UserId { get; set; }
        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string LastName { get; set; }
        [StringLength(15)]
        public string UserName { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [ForeignKey("FK_Users_Roles_RoleId")]
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

        //Navigational Property
       // [JsonIgnore]
       // public virtual ICollection<Topic> Topics { get; set; }
        //Navigational Property
       // [JsonIgnore]
       // public virtual ICollection<Comment> Comments { get; set; }

    }
}
