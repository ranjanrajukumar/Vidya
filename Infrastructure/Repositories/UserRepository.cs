using Vidya.Domain.Entities;
using Vidya.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vidya.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Vidya.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Assuming you have a data context (e.g., a DbContext) for accessing the database
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            // This is just an example assuming you are using Entity Framework.

            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
