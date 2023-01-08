using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Helpers;
using ScannedAPI.Models;

namespace ScannedAPI.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : CommonApiController
    {
        private readonly JwtSettings _jwtSettings;

        public AuthController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /*
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
        }*/
    }
}

