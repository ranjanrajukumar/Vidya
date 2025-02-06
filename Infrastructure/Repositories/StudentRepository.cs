using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Data;

namespace Vidya.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Where(s => s.DelStatus == 0).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId && s.DelStatus == 0);
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int studentId)
        {
            var student = await GetByIdAsync(studentId);
            if (student != null)
            {
                student.DelStatus = 1;  // Soft delete
                await _context.SaveChangesAsync();
            }
        }
    }
}
