namespace ShoppingList.Domain.ShoppingList;

public class ProductEntity
{
    public int Id { get; private set; }
    public int ShoppingListId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductDescription { get; private set; }
    public int Amount { get; private set; }
    public bool Completed { get; private set; }

    private ProductEntity()
    {
    }

    private ProductEntity(int shoppingListId, string productName, string productDescription, int amount)
    {
        ShoppingListId = shoppingListId;
        ProductName = productName;
        ProductDescription = productDescription;
        Amount = amount;
    }

    public static ProductEntity Create(int shoppingListId, string productName, string productDescription, int amount)
        => new(shoppingListId, productName, productDescription, amount);

    public void Complete() => Completed = true;

    public void Uncomplete() => Completed = false;

    public void Update(string productName, string productDescription, int amount, bool completed)
    {
        ProductName = productName;
        ProductDescription = productDescription;
        Amount = amount;
        Completed = completed;
    }
}