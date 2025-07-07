using Application.RepositoriesInterface;

namespace Application.UseCase.CarUseCase.DeleteCar
{
    public class DeleteCar : IDeleteCar
    {
        private readonly IDeleteCarRepository _deleteCarRepository;
        public DeleteCar(IDeleteCarRepository deleteCarRepository)
        {
            _deleteCarRepository = deleteCarRepository;
        }
        public async Task<bool> DeleteCarAsync(Guid id)
        {
            try
            {
                await _deleteCarRepository.DeleteCarAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
