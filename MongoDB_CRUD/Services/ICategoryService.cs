using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync(string userId);
        Task<Category> GetByIdAsync(string userId, string linkId);
        Task<Category> CreateAsync(string userId, Category category);
        Task UpdateAsync(string userId, string linkId, Category category);
        Task DeleteAsync(string userId, string linkId);
    }
}
