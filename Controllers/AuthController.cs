using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Helpers;
using ScannedAPI.Models;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : CommonApiController
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IAuthService authService, JwtSettings jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Login credentials cannot be empty");
            try
            {
                var user = await _authService.ValidateUser(dto);
                if (user == null) throw new Exception("Invalid credentials");

                var userToken = JwtHelper.GenTokenkey(new UserTokens()
                {
                    Email = user.Email,
                    UserId = user.Id,
                    DisplayName = user.FirstName + " " + user.LastName
                }, _jwtSettings);

                return Ok(userToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

