using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Controllers
{
    [Route("auth")]
    public class AuthController : CommonApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] RegisterDto dto)
        {
            try
            {
                var userId = await _authService.Register(dto);
                return Ok(userId);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}

