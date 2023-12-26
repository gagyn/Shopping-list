namespace ShoppingList.Domain.ShoppingList;

public class ProductEntity
{
    public int Id { get; private set; }
    public int ShoppingListId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductDescription { get; private set; }
    public decimal Amount { get; private set; }
    public UnitEntity Unit { get; private set; }
    public bool Completed { get; private set; }

    private ProductEntity()
    {
    }

    private ProductEntity(int shoppingListId, string productName, string productDescription, decimal amount, UnitEntity unit)
    {
        ShoppingListId = shoppingListId;
        ProductName = productName;
        ProductDescription = productDescription;
        Amount = amount;
        Unit = unit;
    }

    public static ProductEntity Create(int shoppingListId, string productName, string productDescription, decimal amount, UnitEntity unit)
        => new(shoppingListId, productName, productDescription, amount, unit);

    public void Complete() => Completed = true;

    public void Uncomplete() => Completed = false;

    public void Update(string productName, string productDescription, decimal amount, bool completed, UnitEntity unit)
    {
        ProductName = productName;
        ProductDescription = productDescription;
        Amount = amount;
        Completed = completed;
        Unit = unit;
    }
}