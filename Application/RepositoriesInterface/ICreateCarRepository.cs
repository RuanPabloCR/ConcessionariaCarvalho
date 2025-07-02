using Domain.Entities;


namespace Application.RepositoriesInterface
{
    public interface ICreateCarRepository
    {
        Task RegisterCarAsync(Car request);
    }
}