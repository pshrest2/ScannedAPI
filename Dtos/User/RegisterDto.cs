using System;
namespace ScannedAPI.Dtos.AuthDtos
{
    public class RegisterDto
    {
        public RegisterDto()
        {
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long Phone { get; set; }
    }
}

