using Application.RepositoriesInterface;
using Domain.Entities;
using Infraestructure.Data;

namespace Infraestructure.Repositories.GuestR
{
    public class GetGuestRepository : IGetGuestRepository
    {
        private readonly AppDbContext _context;
        public GetGuestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guest?> GetGuestByIdAsync(Guid guestId)
        {
            return await _context.Guests.FindAsync(guestId);
        }
            
    }
}
