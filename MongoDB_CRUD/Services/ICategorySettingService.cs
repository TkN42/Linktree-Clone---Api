using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Services
{
    public interface ICategorySettingService
    {
        Task<LinkSettings> GetAllAsync(string linkId);
        Task<LinkSettings> GetByIdAsync(string linkId);
        Task<LinkSettings> CreateAsync(string linkId, LinkSettings linkSettings);
        Task UpdateAsync(string linkId, LinkSettings linkSettings);
        Task DeleteAsync(string linkId);
    }
}
