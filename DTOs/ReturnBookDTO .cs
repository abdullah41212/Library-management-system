using Library_management_system.Models.Database.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Library_management_system.DTOs
{
    public class ReturnBookDTO
    {
        public int id { get; set; }
        public int BookId { get; set; }

        public DateTime? ReturnDate { get; set; } = DateTime.UtcNow;

    }
}
