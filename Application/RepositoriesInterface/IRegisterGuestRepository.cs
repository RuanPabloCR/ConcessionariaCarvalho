using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface IRegisterGuestRepository
    {
        Task<Guest?> GetByEmailAsync(string email);
        Task AddAsync(Guest guest); 
    }
}
