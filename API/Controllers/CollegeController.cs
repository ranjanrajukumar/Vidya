using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vidya.API.DTO;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;

namespace Vidya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeRepository _collegeRepository;

        public CollegeController(ICollegeRepository collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        // GET: api/college
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollegeDTO>>> GetColleges()
        {
            var colleges = await _collegeRepository.GetAllAsync();
            return Ok(colleges);
        }

        // GET: api/college/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CollegeDTO>> GetCollege(int id)
        {
            var college = await _collegeRepository.GetByIdAsync(id);
            if (college == null) return NotFound("College not found");
            return Ok(college);
        }

        // POST: api/college
        [HttpPost]
        public async Task<ActionResult> CreateCollege([FromBody] CollegeDTO collegeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var college = new College
            {
                CollegeName = collegeDto.CollegeName,
                ShortName = collegeDto.ShortName,
                DelStatus = 0
            };

            await _collegeRepository.AddAsync(college);
            return CreatedAtAction(nameof(GetCollege), new { id = college.CollegeId }, college);
        }

        // PUT: api/college/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCollege(int id, [FromBody] CollegeDTO collegeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCollege = await _collegeRepository.GetByIdAsync(id);
            if (existingCollege == null) return NotFound("College not found");

            existingCollege.CollegeName = collegeDto.CollegeName;
            existingCollege.ShortName = collegeDto.ShortName;

            await _collegeRepository.UpdateAsync(existingCollege);
            return NoContent();
        }

        // DELETE: api/college/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCollege(int id)
        {
            var college = await _collegeRepository.GetByIdAsync(id);
            if (college == null) return NotFound("College not found");

            // Soft delete by updating DelStatus to 1
            college.DelStatus = 1;
            await _collegeRepository.UpdateAsync(college);

            return NoContent();
        }

    }
}
