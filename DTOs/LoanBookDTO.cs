using Library_management_system.Models.Database.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Library_management_system.DTOs
{
    public class LoanBookDTO
    {
        public int BookId { get; set; }

        public DateTime DueDate { get; set; }

    }
}
