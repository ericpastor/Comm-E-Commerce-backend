using System.Security.Claims;
using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Comm.Core.src.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    public class UserController : BaseController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserController(IUserService service, IMapper mapper) : base(service)
        {
            _userService = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> CreateOneAsync([FromBody] UserCreateDto userCreateDto)
        {
            return await _userService.CreateOneAsync(userCreateDto);
        }
        [Authorize]
        [HttpPatch("update-profile")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to determine user identity.");
            }

            var existingUser = await _userService.GetByIdAsync(Guid.Parse(userId));

            if (existingUser == null)
            {
                return NotFound();
            }

            return Ok(await _userService.UpdateOneAsync(existingUser.Id, userUpdateDto));
        }

        [HttpGet("profile"), Authorize]
        public async Task<ActionResult<UserReadDto>> GetUserProfile()
        {
            var authenticatedClaims = HttpContext.User;
            var id = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            return await base.GetByIdAsync(Guid.Parse(id));
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllAsync([FromQuery] GetAllParams getAllParams)
        {
            return Ok(await _userService.GetAllAsync(getAllParams));
        }

        [Authorize]
        public override async Task<ActionResult<UserReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }
    }
}