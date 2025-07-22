using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data
{
    public static class SeedCarExtension
    {
        public static void SeedCars(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Cars.Any())
                {
                    var cars = new List<Car>
                    {
                        new Car { Id = Guid.NewGuid(), Model = "Civic", Brand = "Honda", Year = 2022, Price = 90000, Status = CarStatus.Available },
                        new Car { Id = Guid.NewGuid(), Model = "Corolla", Brand = "Toyota", Year = 2023, Price = 95000, Status = CarStatus.Available },
                        new Car { Id = Guid.NewGuid(), Model = "Onix", Brand = "Chevrolet", Year = 2021, Price = 70000, Status = CarStatus.Available }
                    };
                    context.Cars.AddRange(cars);
                    context.SaveChanges();
                }
            }
        }
    }
}
