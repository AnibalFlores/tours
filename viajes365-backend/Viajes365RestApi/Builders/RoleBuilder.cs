using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viajes365RestApi.Entities;

namespace Viajes365RestApi.Builders
{
    public class RoleBuilder : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(u => u.RoleName).IsUnique();
            builder.HasData(
                new Role { RoleId = 1L, RoleName = "Usuario", Active = true },
                new Role { RoleId = 2L, RoleName = "Administrador", Active = true },
                new Role { RoleId = 3L, RoleName = "Moderador", Active = false },
                new Role { RoleId = 4L, RoleName = "Anónimo", Active = false }
                );
        }
    }
}
