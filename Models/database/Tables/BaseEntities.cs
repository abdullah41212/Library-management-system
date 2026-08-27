namespace Library_management_system.Models.Database.Tables;
// note the namespace matches the folder path exactly — this is a C# convention
// (not a hard requirement, but keeps navigation predictable)

public abstract class BaseEntities
{
    // "abstract" — you can never create a BaseEntity directly (new BaseEntity()
    // would be an error). It only exists to be inherited from.

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    // set once when a row is created; UtcNow avoids timezone bugs later

    public DateTime? UpdatedOn { get; set; }
    // "DateTime?" — nullable, because a brand-new row hasn't been updated yet
}