using Comm.Business.src.Interfaces;
using Comm.Core.src.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<string> Login(Credentials credentials)
        {
            return await _service.Login(credentials);
        }
    }
}