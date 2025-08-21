using Microsoft.EntityFrameworkCore;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Data;

namespace Vidya.Infrastructure.Repositories
{
    public class MenuRightsRepository : IMenuRightsRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRightsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuRights>> GetAllAsync()
        {
            return await _context.MenuRights
                         .AsNoTracking()
                         .Where(m => (m.DelStatus ?? 0) == 0)
                         .ToListAsync();
        }

        public async Task<MenuRights?> GetByIdAsync(int id)
        {
            return await _context.MenuRights
                      .FirstOrDefaultAsync(m => m.IDCode == id && (m.DelStatus ?? 0) == 0);

        }

        public async Task AddAsync(MenuRights rights)
        {
            await _context.MenuRights.AddAsync(rights);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuRights rights)
        {
            _context.MenuRights.Update(rights);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rights = await _context.MenuRights.FindAsync(id);
            if (rights != null)
            {
                _context.MenuRights.Remove(rights);
                await _context.SaveChangesAsync();
            }
        }


        public async Task AddOrUpdateMenuRightsAsync(int userId, List<MenuRights> newMenuRights)
        {
            // Step 1: Check if the user already has menu rights
            var existingRights = await _context.MenuRights
                .Where(m => m.UserId == userId && m.DelStatus == 0)
                .ToListAsync();

            if (existingRights.Any())
            {
                // Step 2: Soft delete existing rights
                foreach (var right in existingRights)
                {
                    right.DelStatus = 1;
                    _context.MenuRights.Update(right);
                }
            }

            // Step 3: Insert new rights
            await _context.MenuRights.AddRangeAsync(newMenuRights);

            // Step 4: Save changes
            await _context.SaveChangesAsync();
        }
    }
}
