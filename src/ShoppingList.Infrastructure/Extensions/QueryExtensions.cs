namespace ShoppingList.Infrastructure.Extensions;
public static class QueryExtensions
{
    public static string ShorterDescription(this string description)
        => description.Length > 50 ? description.Substring(0, 50).Trim() + "..." : description;
}
