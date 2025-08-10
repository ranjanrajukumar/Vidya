using Microsoft.AspNetCore.Mvc;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;

namespace Vidya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuListController : ControllerBase
    {
        private readonly IMenuListRepository _menuListRepository;

        public MenuListController(IMenuListRepository menuListRepository)
        {
            _menuListRepository = menuListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _menuListRepository.GetAllAsync();
            return Ok(menus);
        }


        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var menus = await _menuListRepository.GetAllAsync(userId);
            return Ok(menus);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _menuListRepository.GetByIdAsync(id);
            if (menu == null)
                return NotFound();

            return Ok(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuList menuList)
        {
            await _menuListRepository.AddAsync(menuList);
            return CreatedAtAction(nameof(GetById), new { id = menuList.MenuId }, menuList);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuList menuList)
        {
            if (id != menuList.MenuId)
                return BadRequest("Menu ID mismatch.");

            await _menuListRepository.UpdateAsync(menuList);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuListRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
