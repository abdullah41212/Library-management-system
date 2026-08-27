namespace Library_management_system.DTOs
{
    public class AddBookResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int CopiesTotal { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
