using Application.RepositoriesInterface;
using Application.Services.CarPurshaseService;
using Application.Services.PasswordEncryption;
using Application.UseCase.CarUseCase.GetCar;
using Application.UseCase.CarUseCase.RegisterCar;
using Application.UseCase.CarUseCase.UpdateCar;
using Application.UseCase.GuestUseCase.DeleteGuest;
using Application.UseCase.GuestUseCase.GetGuest;
using Application.UseCase.GuestUseCase.LoginGuest;
using Application.UseCase.GuestUseCase.RegisterGuest;
using Application.UseCase.SalesPersonUseCase.DeleteSalesPerson;
using Application.UseCase.SalesPersonUseCase.LoginSalesPerson;
using Application.UseCase.SalesPersonUseCase.RegisterSalesPerson;
using Application.UseCase.SaleUseCase;
using Application.UseCase.SaleUseCase.GetSale;
using Application.Validators;
using FluentValidation;
using Infraestructure.Data;
using Infraestructure.Repositories.CarR;
using Infraestructure.Repositories.GuestR;
using Infraestructure.Repositories.SaleR;
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
            //guests
            services.AddScoped<IRegisterGuestRepository, RegisterGuestRepository>();
            services.AddScoped<IDeleteGuestRepository, DeleteGuestRepository>();
            services.AddScoped<IGetGuestRepository, GetGuestRepository>();

            // cars
            services.AddScoped<ICreateCarRepository, CreateCarRepository>();
            services.AddScoped<IUpdateCarRepository, UpdateCarRepository>();
            services.AddScoped<IGetCarRepository, GetCarRepository>();
            services.AddScoped<IDeleteCarRepository, DeleteCarRepository>();

            // sales persons
            services.AddScoped<IRegisterSalesPersonRepository, RegisterSalesPersonRepository>();
            services.AddScoped<IDeleteSalesPersonRepository, DeleteSalesPersonRepository>();
            services.AddScoped<IGetSalesPersonRepository, GetSalesPersonRepository>();

            // sales
            services.AddScoped<ISalesRepository, SalesRepository>();

            // validators
            services.AddValidatorsFromAssemblyContaining<GuestRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SalesPersonRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CarRequestValidator>();
        }

        public static void AddApplication(this IServiceCollection services)
        {
            // guests
            services.AddScoped<IRegisterGuestUseCase, RegisterGuestUseCase>();
            services.AddScoped<ILoginGuestUseCase, LoginGuestUseCase>();
            services.AddScoped<IDeleteGuestUseCase, DeleteGuestUseCase>();
            services.AddScoped<IGetGuestUseCase, GetGuestUseCase>();
            // sales persons
            services.AddScoped<IRegisterSalesPersonUseCase, RegisterSalesPersonUseCase>();
            services.AddScoped<ILoginSalesPersonUseCase, LoginSalesPersonUseCase>();
            services.AddScoped<IDeleteSalesPersonUseCase, DeleteSalesPersonUseCase>();
            // sales
            services.AddScoped<IRegisterSaleUseCase, RegisterSaleUseCase>();
            services.AddScoped<IGetSaleUseCase, GetSaleUseCase>();
            services.AddScoped<IGetSalesByDateUseCase, GetSalesByDateUseCase>();
            services.AddScoped<IBuyCarUseCase, BuyCarUseCase>();

            // cars
            services.AddScoped<IGetCarUseCase, GetCarUseCase>();
            services.AddScoped<ICreateCarUseCase, CreateCarUseCase>();
            services.AddScoped<IUpdateCarUseCase, UpdateCarUseCase>();

            //
            services.AddScoped<ICarPurschase, CarPurchase>();
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
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
