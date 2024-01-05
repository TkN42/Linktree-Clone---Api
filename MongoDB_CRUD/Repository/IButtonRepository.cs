using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public interface IButtonRepository
    {
        Task<List<LinkButton>> GetAllAsync(string linkId);
        Task<LinkButton> GetByIdAsync(string linkId, string buttonId);
        Task<LinkButton> CreateAsync(string linkId, LinkButton linkButton);
        Task UpdateAsync(string linkId, string buttonId, LinkButton linkButton);
        Task DeleteAsync(string linkId, string buttonId);
    }
}
