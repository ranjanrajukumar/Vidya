using Microsoft.EntityFrameworkCore;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Data;

namespace Vidya.Infrastructure.Repositories
{
    public class CollegeRepository:ICollegeRepository
    {
        private readonly ApplicationDbContext _context;

        public CollegeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<College>> GetAllAsync()
        {
            return await _context.colleges.Where(s => s.DelStatus == 0).ToListAsync();
        }

        public async Task<College?> GetByIdAsync(int collegeId)
        {
            return await _context.colleges.FirstOrDefaultAsync(s => s.CollegeId == collegeId && s.DelStatus == 0);
        }

        public async Task AddAsync(College college)
        {
            await _context.colleges.AddAsync(college);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(College college)
        {
            _context.colleges.Update(college);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int collegeId)
        {
            var college = await GetByIdAsync(collegeId);
            if (college != null)
            {
                college.DelStatus = 1;  // Soft delete
                await _context.SaveChangesAsync();
            }
        }
    }
}
