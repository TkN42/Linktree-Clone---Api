using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;
using Aes = System.Security.Cryptography.Aes;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _linkService.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("{categoryId:length(8)}")]
        public async Task<IActionResult> Get(string categoryId)
        {
            var link = await _linkService.GetByIdAsync(categoryId).ConfigureAwait(false);
            if (link == null)
            {
                return NotFound();
            }
            return Ok(link);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Link link)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest();
        //        }

        //        await _linkService.CreateAsync(link).ConfigureAwait(false);

        //        return Ok(link.Id);
        //    }
        //    catch(Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
            
        //}

        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, Link linkIn)
        //{
        //    var link = await _linkService.GetByIdAsync(id).ConfigureAwait(false);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }
        //    await _linkService.UpdateAsync(id, linkIn).ConfigureAwait(false);
        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var link = await _linkService.GetByIdAsync(id).ConfigureAwait(false);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }
        //    await _linkService.DeleteAsync(link.Id).ConfigureAwait(false);
        //    return NoContent();
        //}

    }
}
