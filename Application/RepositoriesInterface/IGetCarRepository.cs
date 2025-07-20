using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface IGetCarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<IEnumerable<Car>> GetCarsByDateAsync(DateOnly data);
        Task<IEnumerable<Car>> SearchAsync(String searchTerm);
    }
}
