using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Services.PasswordEncryption;

namespace Infraestructure.Data
{
    public static class SeedAdminExtension
    {
        public static void SeedAdmin(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordEncryptionService>();
                context.Database.Migrate();

                if (!context.Admins.Any(a => a.Email == "Admin@gmail.com"))
                {
                    var admin = new Admin
                    {
                        Id = Guid.NewGuid(),
                        Email = "Admin@gmail.com",
                        Password = passwordService.HashPassword("Admin123")
                    };
                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
            }
        }
    }
}
