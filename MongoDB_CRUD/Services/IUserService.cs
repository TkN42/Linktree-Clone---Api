using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        bool GetByKeyAsync(string key);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(string id, User userIn, User user);
        Task DeleteAsync(string id);
    }
}
