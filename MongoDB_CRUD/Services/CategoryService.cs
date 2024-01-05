using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<Category>> GetAllAsync(string userId)
        {
            return _categoryRepository.GetAllAsync(userId);
        }

        public Task<Category> GetByIdAsync(string userId, string linkId)
        {
            return _categoryRepository.GetByIdAsync(userId, linkId);
        }

        public Task<Category> CreateAsync(string userId, Category category)
        {
            return _categoryRepository.CreateAsync(userId, category);
        }

        public Task UpdateAsync(string userId, string linkId, Category category)
        {
            return _categoryRepository.UpdateAsync(userId, linkId, category);
        }

        public Task DeleteAsync(string userId, string linkId)
        {
            return _categoryRepository.DeleteAsync(userId, linkId);
        }
    }
}
