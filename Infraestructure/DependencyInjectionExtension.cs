using Application.RepositoriesInterface;
using Application.UseCase.GuestUseCase.LoginGuest;
using Application.UseCase.GuestUseCase.RegisterGuest;
using Application.UseCase.SalesPersonUseCase.DeleteSalesPerson;
using Application.UseCase.SalesPersonUseCase.LoginSalesPerson;
using Application.UseCase.SalesPersonUseCase.RegisterSalesPerson;
using Application.Validators;
using FluentValidation;
using Infraestructure.Data;
using Infraestructure.Repositories.CarR;
using Infraestructure.Repositories.GuestR;
using Infraestructure.Repositories.SalesPersonR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddScoped<IRegisterGuestRepository, RegisterGuestRepository>();
            services.AddScoped<IDeleteGuestRepository, DeleteGuestRepository>();
            services.AddScoped<ICreateCarRepository, CreateCarRepository>();
            services.AddScoped<IUpdateCarRepository, UpdateCarRepository>();
            services.AddScoped<IRegisterSalesPersonRepository, RegisterSalesPersonRepository>();
            services.AddScoped<IDeleteSalesPersonRepository, DeleteSalesPersonRepository>();

            services.AddValidatorsFromAssemblyContaining<GuestRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SalesPersonRequestValidator>();
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterGuestUseCase, RegisterGuestUseCase>();
            services.AddScoped<ILoginGuestUseCase, LoginGuestUseCase>();
            services.AddScoped<IRegisterSalesPersonUseCase, RegisterSalesPersonUseCase>();
            services.AddScoped<ILoginSalesPersonUseCase, LoginSalesPersonUseCase>();
            services.AddScoped<IDeleteSalesPersonUseCase, DeleteSalesPersonUseCase>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
