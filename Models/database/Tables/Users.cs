using Library_management_system.Enums;
using Library_management_system.Models.Database.Tables;

public class Users : BaseEntities
{
    public int Id { get; set; }


    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public UserTypes UserType { get; set; } 

    public ICollection<Loans> Loans { get; set; } = new List<Loans>();

}