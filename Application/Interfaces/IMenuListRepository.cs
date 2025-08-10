using System.Collections.Generic;
using System.Threading.Tasks;
using Vidya.Domain.Entities;

namespace Vidya.Application.Interfaces
{
    public interface IMenuListRepository
    {
        Task<IEnumerable<MenuList>> GetAllAsync();
        Task<IEnumerable<MenuList>> GetAllAsync(int userId);
        Task<MenuList?> GetByIdAsync(int menuId);
        Task AddAsync(MenuList menuList);
        Task UpdateAsync(MenuList menuList);
        Task DeleteAsync(int menuId);
    }
}
