using Application.RepositoriesInterface;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class DeleteGuestRepository : IDeleteGuestRepository
    {   
        private readonly AppDbContext _context;
        public DeleteGuestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(Guid guestId)
        {
           var guest = await _context.Guests.FindAsync(guestId);
           if(guest != null)
           {
               _context.Guests.Remove(guest);
               await _context.SaveChangesAsync();
           }
        }
    }
}
