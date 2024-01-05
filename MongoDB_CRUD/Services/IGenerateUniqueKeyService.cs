using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Services
{
    public interface IGenerateUniqueKeyService
    {
        string GenerateUniqueKey();
    }
}
