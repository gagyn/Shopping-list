using ShoppingList.Domain.Base;

namespace ShoppingList.Domain.ShoppingList;
public class ShoppingListEntity : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<ProductEntity> Products { get; private set; }
    public Guid OwnedByUserId { get; private set; }

    private ShoppingListEntity()
    {
    }

    private ShoppingListEntity(string name, Guid ownedByUserId, string createdBy) : base(createdBy)
    {
        Name = name;
        OwnedByUserId = ownedByUserId;
    }

    public static ShoppingListEntity Create(string name, Guid ownedByUserId, string createdBy)
        => new(name, ownedByUserId, createdBy);

    public void AddProduct(string productName, string productDescription, int amount, string modifiedBy)
    {
        var product = ProductEntity.Create(Id, productName, productDescription, amount);
        Products.Add(product);
        SetModified(modifiedBy);
    }

    public void UpdateName(string name, string modifiedBy)
    {
        Name = name;
        this.SetModified(modifiedBy);
    }
}
