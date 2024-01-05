using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class CategorySettingService : ICategorySettingService
    {
        private readonly ICategorySettingRepository _categorySettingRepository;

        public CategorySettingService(ICategorySettingRepository categorySettingRepository)
        {
            _categorySettingRepository = categorySettingRepository;
        }

        public Task<LinkSettings> GetAllAsync(string linkId)
        {
            return _categorySettingRepository.GetAllAsync(linkId);
        }

        public Task<LinkSettings> GetByIdAsync(string linkId)
        {
            return _categorySettingRepository.GetByIdAsync(linkId);
        }

        public Task<LinkSettings> CreateAsync(string linkId, LinkSettings linkSettings)
        {
            return _categorySettingRepository.CreateAsync(linkId, linkSettings);
        }

        public Task UpdateAsync(string linkId, LinkSettings linkSettings)
        {
            return _categorySettingRepository.UpdateAsync(linkId, linkSettings);
        }

        public Task DeleteAsync(string linkId)
        {
            return _categorySettingRepository.DeleteAsync(linkId);
        }
    }
}
