using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.RepositoriesInterface;
using Infraestructure.Repositories;
namespace Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {   services.AddScoped<IRegisterGuestRepository, RegisterGuestRepository>();
            services.AddScoped<IDeleteGuestRepository, DeleteGuestRepository>();
            services.AddScoped<ICreateCarRepository, CreateCarRepository>();
            services.AddScoped<IUpdateCarRepository, UpdateCarRepository>();
            services.AddScoped<IRegisterSalesPersonRepository, RegisterSalesPersonRepository>();
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
