using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButtonController : ControllerBase
    {
        private readonly IButtonService _buttonService;
        private readonly ILinkService _linkService;
        private readonly IGenerateUniqueKeyService _generateUniqueKeyService;
        public ButtonController(IButtonService buttonService, ILinkService linkService, IGenerateUniqueKeyService generateUniqueKeyService)
        {
            _buttonService = buttonService;
            _linkService = linkService;
            _generateUniqueKeyService = generateUniqueKeyService;
        }

        [HttpGet("{categoryId:length(8)}")]
        public async Task<IActionResult> GetAll(string categoryId)
        {
            return Ok(await _buttonService.GetAllAsync(categoryId).ConfigureAwait(false));
        }

        [HttpGet("{categoryId:length(8)}/{buttonId:length(8)}")]
        public async Task<IActionResult> Get(string categoryId, string buttonId)
        {
            var linkButton = await _buttonService.GetByIdAsync(categoryId, buttonId).ConfigureAwait(false);
            if (linkButton == null)
            {
                return NotFound();
            }
            return Ok(linkButton);
        }

        [HttpPost("{categoryId:length(8)}")]
        public async Task<IActionResult> Create(string categoryId, LinkButton linkButton)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                bool isKeyUnique = true;
                string newKey = "";
                while (isKeyUnique)
                {
                    //newKey = GenerateUniqueKey();
                    newKey = _generateUniqueKeyService.GenerateUniqueKey();
                    isKeyUnique = _linkService.GetByKeyAsync(newKey);
                }

                linkButton.Id = newKey;
                //default bir atama yapılack mı ???

                await _buttonService.CreateAsync(categoryId, linkButton).ConfigureAwait(false);
                return Ok(linkButton.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{categoryId:length(8)}/{buttonId:length(8)}")]
        public async Task<IActionResult> Update(string categoryId, string buttonId, LinkButton linkButtonIn)
        {
            var linkButton = await _buttonService.GetByIdAsync(categoryId, buttonId).ConfigureAwait(false);
            if (linkButton == null)
            {
                return NotFound();
            }
            try
            {
                //linkButtonIn.Categories = linkButton.Categories;
                linkButtonIn.Id = linkButton.Id;

                if (linkButtonIn.Title == null)
                {
                    linkButtonIn.Title = linkButton.Title;
                }
                if (linkButtonIn.Link == null)
                {
                    linkButtonIn.Link = linkButton.Link;
                }
                if (linkButtonIn.Icon == null)
                {
                    linkButtonIn.Icon = linkButton.Icon;
                }
                if (linkButtonIn.Color == null)
                {
                    linkButtonIn.Color = linkButton.Color;
                }

                await _buttonService.UpdateAsync(categoryId, buttonId, linkButtonIn).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        [HttpDelete("{categoryId:length(8)}/{buttonId:length(8)}")]
        public async Task<IActionResult> Delete(string categoryId, string buttonId)
        {
            var linkButton = await _buttonService.GetByIdAsync(categoryId, buttonId).ConfigureAwait(false);
            if (linkButton == null)
            {
                return NotFound();
            }
            await _buttonService.DeleteAsync(categoryId, buttonId).ConfigureAwait(false);
            return Ok();
        }


    }
}
