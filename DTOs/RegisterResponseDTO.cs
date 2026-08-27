using Library_management_system.Enums;

namespace Library_management_system.DTOs
{
    public class RegisterResponseDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserTypes UserType { get; set; }

    }
}
