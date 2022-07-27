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
    [Route("api/users")]
    public class UsersController : CommonApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;

        public UsersController(IConfiguration configuration, IUsersService usersService)
        {
            _configuration = configuration;
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

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("DisplayName", String.Concat(user.FirstName, user.LastName)),
                        new Claim("Email", user.Email),
                        new Claim("Phone", user.Phone)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);


                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

