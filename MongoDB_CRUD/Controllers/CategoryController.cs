using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategorySettingService _categorySettingService;
        private readonly IUserService _userService;
        private readonly ILinkService _linkService;
        private readonly IGenerateUniqueKeyService _generateUniqueKeyService;
        public CategoryController(ICategoryService categoryService, ICategorySettingService categorySettingService, IUserService userService, ILinkService linkService, IGenerateUniqueKeyService generateUniqueKeyService)
        {
            _categoryService = categoryService;
            _categorySettingService = categorySettingService;
            _userService = userService;
            _linkService = linkService;
            _generateUniqueKeyService = generateUniqueKeyService;
        }

        [HttpGet("{userId:length(24)}")]
        public async Task<IActionResult> GetAll(string userId)
        {
            return Ok(await _categoryService.GetAllAsync(userId).ConfigureAwait(false));
        }

        [HttpGet("{userId:length(24)}/{categoryId:length(8)}")]
        public async Task<IActionResult> Get(string userId, string categoryId)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(userId, categoryId).ConfigureAwait(false);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId:length(24)}")]
        public async Task<IActionResult> Create(string userId, CategoryRequest category)
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
                    isKeyUnique = _userService.GetByKeyAsync(newKey);
                }

                category.Id = newKey;
                var link = new Link { Id = newKey };
                //default bir atama yapılack mı ???
                var cate = new Category { Id = category.Id, CategoryName = category.CategoryName };
                var setting = new LinkSettings { Color = category.Color, BackgroundColor = category.BackgroundColor };
                await _linkService.CreateAsync(link).ConfigureAwait(false);
                await _categoryService.CreateAsync(userId, cate).ConfigureAwait(false);
                await _categorySettingService.CreateAsync(newKey, setting).ConfigureAwait(false);

                //await _categoryService.CreateAsync(userId, category).ConfigureAwait(false);
                return Ok(category.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId:length(24)}/{categoryId:length(8)}")]           // CategoryRequest e göre revize et ( update-delete )     **************************************************
        public async Task<IActionResult> Update(string userId, string categoryId, Category categoryIn)
        {
            var category = await _categoryService.GetByIdAsync(userId, categoryId).ConfigureAwait(false);
            if (category == null)
            {
                return NotFound();
            }
            categoryIn.Id = category.Id;
            await _categoryService.UpdateAsync(userId, categoryId, categoryIn).ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete("{userId:length(24)}/{categoryId:length(8)}")]
        public async Task<IActionResult> Delete(string userId, string categoryId)
        {
            var category = await _categoryService.GetByIdAsync(userId, categoryId).ConfigureAwait(false);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteAsync(userId, categoryId).ConfigureAwait(false);
            return Ok();
        }


    }
}
