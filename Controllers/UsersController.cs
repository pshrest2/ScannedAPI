using Microsoft.AspNetCore.Mvc;
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
    }
}

