using Microsoft.EntityFrameworkCore;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Data;

namespace Vidya.Infrastructure.Repositories
{
    public class MenuListRepository : IMenuListRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuList>> GetAllAsync()
        {
            var menus = await _context.MenuLists
               .OrderBy(m => m.SortOrder)
                 .ToListAsync();

            // Build a lookup for parent-child relationships
            var menuLookup = menus.ToLookup(m => m.ParentId);

            foreach (var menu in menus)
            {
                menu.SubMenus = menuLookup[menu.MenuId].ToList();
            }

            // Return only top-level menus (ParentId == null)
            return menus.Where(m => m.ParentId == 0);
        }

        public async Task<IEnumerable<MenuList>> GetAllAsync(int userId)
        {
            var menus = await _context.MenuRights
        .Where(r => r.UserId == userId && r.DelStatus == 0)
        .Join(
            _context.MenuLists.Where(m => m.DelStatus == 0),
            r => r.MenuID,
            m => m.MenuId,
            (r, m) => new MenuList
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                ParentId = m.ParentId,
                SortOrder = m.SortOrder,
                DelStatus = r.DelStatus ?? 0
            }
        )
        .OrderBy(m => m.SortOrder)
        .ToListAsync();

            // Build lookup for parent-child relationships
            var menuLookup = menus.ToLookup(m => m.ParentId);

            foreach (var menu in menus)
            {
                menu.SubMenus = menuLookup[menu.MenuId].ToList();
            }

            // Return only top-level menus
            return menus.Where(m => m.ParentId == 0).ToList();

        }


        public async Task<MenuList?> GetByIdAsync(int menuId)
        {
            return await _context.MenuLists.FindAsync(menuId);
        }

        public async Task AddAsync(MenuList menuList)
        {
            _context.MenuLists.Add(menuList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuList menuList)
        {
            _context.MenuLists.Update(menuList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int menuId)
        {
            var menu = await _context.MenuLists.FindAsync(menuId);
            if (menu != null)
            {
                _context.MenuLists.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
