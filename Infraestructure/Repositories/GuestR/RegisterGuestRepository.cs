using Application.RepositoriesInterface;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.GuestR

{
    public class RegisterGuestRepository : IRegisterGuestRepository
    {
        private readonly AppDbContext _context;

        public RegisterGuestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guest?> GetByEmailAsync(string email)
        {
            return await _context.Guests.FirstOrDefaultAsync(g => g.Email == email);
        }

        public async Task AddAsync(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }
    }
}