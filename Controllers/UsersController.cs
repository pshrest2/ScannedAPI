using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Controllers
{
    [Route("users")]
    public class UsersController : CommonApiController
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
    }
}

