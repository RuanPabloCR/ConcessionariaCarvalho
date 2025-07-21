using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface IGetGuestRepository
    {
        Task<Guest?> GetGuestByIdAsync(Guid id);
    }
}
