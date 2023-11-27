namespace ShoppingList.Domain.Base;
public abstract class BaseEntity
{
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public string? ModifiedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    protected BaseEntity()
    {
    }

    protected BaseEntity(string createdBy)
    {
        CreatedBy = createdBy;
    }

    protected void SetModified(string modifiedBy)
    {
        ModifiedBy = modifiedBy;
        ModifiedAt = DateTime.Now;
    }
}
