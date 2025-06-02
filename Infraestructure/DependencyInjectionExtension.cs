using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddDbContext(services);
        }
        private static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=ConcessionariaCarvalho;User=root;Password=123456",
                    new MySqlServerVersion(new Version(8, 0, 25)));
            });
        }
    }
}
