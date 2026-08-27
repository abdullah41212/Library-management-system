using Library_management_system.Enums;

namespace Library_management_system.DTOs
{
    public class RegisterUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserTypes UserType { get; set; } 
    }
}
