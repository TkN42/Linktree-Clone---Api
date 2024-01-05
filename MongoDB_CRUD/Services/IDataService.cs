﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Services
{
    public interface IDataService
    {
        Task<List<LinkData>> GetAllAsync();
        Task<LinkData> GetByIdAsync(string id);
        Task<LinkData> CreateAsync(LinkData linkData);
        Task UpdateAsync(string id, LinkData linkData);
        Task DeleteAsync(string id);
    }
}
