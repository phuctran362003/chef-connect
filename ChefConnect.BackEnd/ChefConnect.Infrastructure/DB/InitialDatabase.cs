using ChefConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChefConnect.Infrastructure.DB
{
    public static class InitialDatabase
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ChefConnectDbContext>())
            {
                // Apply pending migrations
                context.Database.Migrate();

                // Seed data for Role table
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role
                        {
                            Name = "Admin",
                            Description = "Administrator role",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new Role
                        {
                            Name = "Chef",
                            Description = "Chef role",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new Role
                        {
                            Name = "Customer",
                            Description = "Customer role",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
