using Application.RepositoriesInterface;
using Domain.Entities;
namespace Application.UseCase.CarUseCase.GetCar
{
    public class GetCarUseCase : IGetCarUseCase
    {
        private readonly IGetCarRepository _getCarRepository;
        public GetCarUseCase(IGetCarRepository getCarRepository)
        {
            _getCarRepository = getCarRepository;
        }
        public async Task<IEnumerable<Car>> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Car>();
            return await _getCarRepository.SearchAsync(searchTerm);
        }
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _getCarRepository.GetAllCarsAsync();
        }
        public async Task<IEnumerable<Car>> GetCarsByDateAsync(DateOnly date)
        {
            return await _getCarRepository.GetCarsByDateAsync(date);
        }
    }
}
