﻿using MeowSpace.Application.Interfaces.Identity;
using MeowSpace.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeowSpace.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest user)
        {
            var result = await _authService.RefreshTokenAsync(user);
            return Ok(result);
        }
    }
}
