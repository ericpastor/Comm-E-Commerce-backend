using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Comm.Core.src.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class BaseController<T, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
        where T : BaseEntity
    {
        private readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _service;
        public BaseController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> service) // constructor para poder inyectar
        {
            _service = service;
        }

        [Authorize]
        [HttpPost()]
        public virtual async Task<ActionResult<TReadDto>> CreateOneAsync([FromBody] TCreateDto createDto)
        {
            var createdObject = await _service.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(CreateOneAsync), createdObject);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public virtual async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return Ok(await _service.DeleteOneAsync(id));
        }

        [HttpGet()]
        public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetAllAsync([FromQuery] GetAllParams getAllParams)
        {
            return Ok(await _service.GetAllAsync(getAllParams));
        }


        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [Authorize]
        [HttpPatch("{id:guid}")]
        public virtual async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] TUpdateDto upDateObject)
        {
            return Ok(await _service.UpdateOneAsync(id, upDateObject));
        }
    }
}