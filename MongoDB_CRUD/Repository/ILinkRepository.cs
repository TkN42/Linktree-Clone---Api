using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public interface ILinkRepository
    {
        Task<List<Link>> GetAllAsync();
        Task<Link> GetByIdAsync(string id);
        bool GetByKeyAsync(string key);
        Task<Link> CreateAsync(Link link);
        Task UpdateAsync(string id, Link link);
        Task DeleteAsync(string id);
    }
}
