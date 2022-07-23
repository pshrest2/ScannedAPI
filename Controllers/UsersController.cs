using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] RegisterDto dto)
        {
            try
            {
                var userId = await _usersService.Register(dto);
                return Ok(userId);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Login credentials cannot be empty");
            try
            {
                var user = await _usersService.Get(dto);
                if (user == null) throw new Exception("Invalid credentials");

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

