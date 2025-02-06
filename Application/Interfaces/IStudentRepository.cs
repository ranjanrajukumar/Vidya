using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vidya.Domain.Entities;

namespace Vidya.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int studentId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int studentId);
    }
}
