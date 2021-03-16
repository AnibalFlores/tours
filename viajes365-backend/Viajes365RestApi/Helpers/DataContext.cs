using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Viajes365RestApi.Builders;
using Viajes365RestApi.Entities;

namespace Viajes365RestApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string _currentUser;

        public DataContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // conectar al sql server express
            options.UseSqlServer(Configuration.GetConnectionString("Viajar365Database"));
        }

        // DBSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
       

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        // Specify DbSet properties etc
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Call Fluent API configurations from here
            // Property Configurations SQL Server Express 2019                    
            new UserBuilder().Configure(mb.Entity<User>());
            new RoleBuilder().Configure(mb.Entity<Role>());

        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;
            if (_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) == null)
                _currentUser = "1";
            else
                _currentUser = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in entries)
            {
                // para las entidades que heredan de abstract BaseModel,
                // les ponemos fechas de creacion, actualizacion (y active en true solo al crear) 
                if (entry.Entity is Base trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // poner fecha de ahora a UpdatedDate
                            trackable.Updated = utcNow;
                            trackable.LastId = long.Parse(_currentUser);

                            // marca la propiedad como no tocar
                            // no actualizar la fecha de creacion en una actualizacion
                            entry.Property("Created").IsModified = false;
                            break;

                        case EntityState.Added:
                            // estamos creando fijamos ambos valores a la fecha de ahora
                            trackable.Created = utcNow;
                            trackable.Updated = utcNow;
                            trackable.CreatorId = long.Parse(_currentUser);
                            trackable.LastId = long.Parse(_currentUser);
                            trackable.Active = true;
                            break;
                    }
                }
            }
        }



    }
}
