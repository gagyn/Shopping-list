using System.ComponentModel;
using System.Reflection;

namespace ShoppingList.Infrastructure.Extensions;
public static class EnumExtensions
{
    public static TResult Parse<TResult>(this Enum sourceEnum) where TResult : struct, Enum
        => Enum.Parse<TResult>(sourceEnum.ToString());

    public static TResult Parse<TResult>(this string enumValue) where TResult : struct, Enum
        => Enum.Parse<TResult>(enumValue);

    public static Dictionary<string, string> GetDescriptions<T>() where T : struct, Enum
    {
        var dict = new Dictionary<string, string>();
        var names = Enum.GetNames<T>();
        foreach (var name in names)
        {
            var field = typeof(T).GetField(name);
            var fds = field!.GetCustomAttribute<DescriptionAttribute>(true);
            dict.Add(name, fds!.Description);
        }
        return dict;
    }
}
