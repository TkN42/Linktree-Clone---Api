using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataService.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var linkData = await _dataService.GetByIdAsync(id).ConfigureAwait(false);
            if (linkData == null)
            {
                return NotFound();
            }
            return Ok(linkData);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LinkData linkData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _dataService.CreateAsync(linkData).ConfigureAwait(false);
            return Ok(linkData.Id);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, LinkData linkDataIn)
        {
            var linkData = await _dataService.GetByIdAsync(id).ConfigureAwait(false);
            if (linkData == null)
            {
                return NotFound();
            }
            await _dataService.UpdateAsync(id, linkDataIn).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var linkData = await _dataService.GetByIdAsync(id).ConfigureAwait(false);
            if (linkData == null)
            {
                return NotFound();
            }
            await _dataService.DeleteAsync(linkData.Id).ConfigureAwait(false);
            return NoContent();
        }

    }
}
