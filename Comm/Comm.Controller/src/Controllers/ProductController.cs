using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    [Route("api/v1/[controller]s")]
    public class ProductController : BaseController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        private IProductService _productService;

        public ProductController(IProductService service) : base(service)
        {
            _productService = service;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductReadDto>> CreateOneAsync([FromBody] ProductCreateDto productCreateDto)
        {
            return await base.CreateOneAsync(productCreateDto);
        }


        [HttpPatch("update/{id:guid}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _productService.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            return Ok(await _productService.UpdateOneAsync(id, productUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return await base.DeleteOneAsync(id);
        }
    }
}
