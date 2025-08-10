using Vidya.Domain.Entities;

namespace Vidya.Application.Interfaces
{
    public interface IMenuRightsRepository
    {
        Task<IEnumerable<MenuRights>> GetAllAsync();
        Task<MenuRights?> GetByIdAsync(int menuId);
        Task AddAsync(MenuRights menuRights);
        Task UpdateAsync(MenuRights menuRights);
        Task DeleteAsync(int IDCode);

        Task AddOrUpdateMenuRightsAsync(int userId, List<MenuRights> newMenuRights);
    }
}
