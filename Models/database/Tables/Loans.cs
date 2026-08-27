using Library_management_system.Models.Database.Tables;

namespace Library_management_system.Models.Database.Tables;

public class Loans : BaseEntities
{
    public int Id { get; set; }

    public int BookId { get; set; }
    // "foreign key" property — just an int, holds the Id of the related Book row
    public Book Book { get; set; } = null!;
    // "navigation property" — lets you write loan.Book.Title instead of a
    // separate lookup query. EF Core matches this to BookId automatically
    // by naming convention (BookId + Book together = one relationship).
    // "= null!" — tells the compiler "this will be set by EF, trust me,
    // don't warn me it's uninitialized" (it's nullable-reference-safe in practice
    // because EF always populates it when you .Include() the related data)

    public int UserId { get; set; }
    public Users User { get; set; } = null!;

    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    // null = still borrowed; a real date = returned
}