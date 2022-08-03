using System.ComponentModel.DataAnnotations;

namespace ScannedAPI.Dtos.AuthDtos
{
    public class RegisterDto
    {
        public RegisterDto()
        {
        }
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}

