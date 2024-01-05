using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySettingController : ControllerBase
    {
        private readonly ICategorySettingService _categorySettingService;
        public CategorySettingController(ICategorySettingService categorySettingService)
        {
            _categorySettingService = categorySettingService;
        }

        [HttpGet("{categoryId:length(8)}")]
        public async Task<IActionResult> GetAll(string categoryId)
        {
            return Ok(await _categorySettingService.GetAllAsync(categoryId).ConfigureAwait(false));
        }

        //[HttpGet("{categoryId:length(8)}")]
        //public async Task<IActionResult> Get(string categoryId)
        //{
        //    var linkSettings = await _categorySettingService.GetByIdAsync(categoryId).ConfigureAwait(false);
        //    if (linkSettings == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(linkSettings);
        //}

        [HttpPost("{categoryId:length(8)}")]
        public async Task<IActionResult> Create(string categoryId, LinkSettings linkSettings)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                //default bir atama yapılack mı ???

                await _categorySettingService.CreateAsync(categoryId, linkSettings).ConfigureAwait(false);
                return Ok(linkSettings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{categoryId:length(8)}")]
        public async Task<IActionResult> Update(string categoryId, LinkSettings linkSettingsIn)
        {
            var linkSettings = await _categorySettingService.GetByIdAsync(categoryId).ConfigureAwait(false);
            if (linkSettings == null)
            {
                return NotFound();
            }
            try
            {
                if (linkSettingsIn.Color == null)
                {
                    linkSettingsIn.Color = linkSettings.Color;
                }
                if (linkSettingsIn.BackgroundColor == null)
                {
                    linkSettingsIn.BackgroundColor = linkSettings.BackgroundColor;
                }

                await _categorySettingService.UpdateAsync(categoryId, linkSettingsIn).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        [HttpDelete("{categoryId:length(8)}")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var linkSettings = await _categorySettingService.GetByIdAsync(categoryId).ConfigureAwait(false);
            if (linkSettings == null)
            {
                return NotFound();
            }
            await _categorySettingService.DeleteAsync(categoryId).ConfigureAwait(false);
            return Ok();
        }


    }
}
