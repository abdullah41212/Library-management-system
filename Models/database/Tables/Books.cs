
namespace Library_management_system.Models.Database.Tables;

public class Book : BaseEntities
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int CopiesTotal { get; set; }
    public int CopiesAvailable { get; set; }

    public int BookStatus { get; set; }
    public ICollection<Loans> Loans { get; set; } = new List<Loans>();
}