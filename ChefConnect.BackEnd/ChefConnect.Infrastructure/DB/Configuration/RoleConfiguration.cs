using ChefConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChefConnect.Infrastructure.DB.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {


            // Seed Data
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Administrator role"
                },
                new Role
                {
                    Id = 2,
                    Name = "Chef",
                    Description = "Chef role"
                },
                new Role
                {
                    Id = 3,
                    Name = "Customer",
                    Description = "Customer role"
                }
            );
        }
    }
}
