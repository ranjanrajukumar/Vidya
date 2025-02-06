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
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound("Student not found");
            return Ok(student);
        }

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromBody] StudentDTO studentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var student = new Student
            {
                FirstName = studentDto.FirstName,
                MiddleName = studentDto.MiddleName,
                LastName = studentDto.LastName,
                DOB = studentDto.DOB,
                Gender = studentDto.Gender,
                EmailId = studentDto.EmailId,
                Mobile = studentDto.Mobile,
                DelStatus = 0
            };

            await _studentRepository.AddAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null) return NotFound("Student not found");

            existingStudent.FirstName = studentDto.FirstName;
            existingStudent.MiddleName = studentDto.MiddleName;
            existingStudent.LastName = studentDto.LastName;
            existingStudent.DOB = studentDto.DOB;
            existingStudent.Gender = studentDto.Gender;
            existingStudent.EmailId = studentDto.EmailId;
            existingStudent.Mobile = studentDto.Mobile;

            await _studentRepository.UpdateAsync(existingStudent);
            return NoContent();
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound("Student not found");

            await _studentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
