using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Controller.src.Utilities;
using Comm.Core.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController : BaseController<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService service) : base(service)
        {
            _categoryService = service;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<CategoryReadDto>> CreateOneAsync([FromBody] CategoryCreateDto categoryCreateDto)
        {
            return await base.CreateOneAsync(categoryCreateDto);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var existingCategory = await _categoryService.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            return Ok(await base.UpdateOneAsync(id, categoryUpdateDto));
        }

        // [HttpGet("ByName/{categoryName}")]
        // public async Task<ActionResult<CategoryReadDto>> GetCategoryWithProductsAsync(string categoryName)
        // {
        //     var categoryWithProducts = await _categoryService.GetCategoryWithProductsAsync(categoryName);
        //     return Ok(categoryWithProducts);
        // }
    }
}