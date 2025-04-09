using MeowSpace.Application.Interfaces.Identity;
using MeowSpace.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeowSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(AuthRequest user)
        {
            var result = await _authService.Login(user);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest user)
        {
            var result = await _authService.Register(user);
            return Ok(result);
        }
    }
}
