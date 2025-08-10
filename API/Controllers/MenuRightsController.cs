using Microsoft.AspNetCore.Mvc;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;

namespace Vidya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuRightsController : ControllerBase
    {
        private readonly IMenuRightsRepository _menuRightsRepository;

        public MenuRightsController(IMenuRightsRepository menuRightsRepository)
        {
            _menuRightsRepository = menuRightsRepository;
        }

        // GET: api/MenuRights
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rights = await _menuRightsRepository.GetAllAsync();
            return Ok(rights);
        }

        // GET: api/MenuRights/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var right = await _menuRightsRepository.GetByIdAsync(id);
            if (right == null)
                return NotFound();

            return Ok(right);
        }

        // POST: api/MenuRights
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuRights menuRights)
        {
            if (menuRights == null)
                return BadRequest("Invalid data.");

            await _menuRightsRepository.AddAsync(menuRights);
            return CreatedAtAction(nameof(GetById), new { id = menuRights.IDCode }, menuRights);
        }

        // PUT: api/MenuRights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuRights menuRights)
        {
            if (id != menuRights.IDCode)
                return BadRequest("ID mismatch.");

            var existing = await _menuRightsRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _menuRightsRepository.UpdateAsync(menuRights);
            return NoContent();
        }

        // DELETE: api/MenuRights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _menuRightsRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _menuRightsRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignMenuRights(int userId, [FromBody] List<MenuRights> menuRights)
        {
            if (menuRights == null || !menuRights.Any())
                return BadRequest("Menu rights list cannot be empty.");

            await _menuRightsRepository.AddOrUpdateMenuRightsAsync(userId, menuRights);

            return Ok(new { message = "Menu rights updated successfully." });
        }
    }
}
