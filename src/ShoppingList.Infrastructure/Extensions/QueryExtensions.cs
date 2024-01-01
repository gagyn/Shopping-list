namespace ShoppingList.Infrastructure.Extensions;
public static class QueryExtensions
{
    public static string ShorterDescription(this string description)
        => description.Length > 100 ? description.Substring(0, 100).Trim() + "..." : description;
}
