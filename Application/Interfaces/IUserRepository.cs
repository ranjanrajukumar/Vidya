using Vidya.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vidya.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task<Users> GetUserByUsernameAsync(string username); // Added for authentication
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);  // Add this line to the interface

    }
}
