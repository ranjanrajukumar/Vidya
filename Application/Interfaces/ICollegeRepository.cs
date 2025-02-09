using Vidya.Domain.Entities;

namespace Vidya.Application.Interfaces
{
    public interface ICollegeRepository
    {
        Task<IEnumerable<College>> GetAllAsync();
        Task<College?> GetByIdAsync(int CollegeId);
        Task AddAsync(College college);
        Task UpdateAsync(College college);
        Task DeleteAsync(int CollegeId);
    }
}
