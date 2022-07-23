namespace ScannedAPI.Dtos.AuthDtos
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string HashedPassword { get; set; }
    }
}

