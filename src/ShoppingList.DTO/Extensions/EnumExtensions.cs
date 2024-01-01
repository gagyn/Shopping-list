using System.ComponentModel;
using System.Reflection;

namespace ShoppingList.DTO.Extensions;
public static class EnumExtensions
{
    public static TResult Parse<TResult>(this Enum sourceEnum) where TResult : struct, Enum
        => Enum.Parse<TResult>(sourceEnum.ToString());

    public static TResult Parse<TResult>(this string enumValue) where TResult : struct, Enum
        => Enum.Parse<TResult>(enumValue);

    public static Dictionary<string, string> GetDescriptions<T>() where T : struct, Enum
    {
        var dict = new Dictionary<string, string>();
        var names = Enum.GetValues<T>();
        foreach (var enumValue in names)
        {
            dict.Add(enumValue.ToString(), enumValue.GetDescription());
        }
        return dict;
    }

    public static string GetDescription<T>(this T enumValue) where T : struct, Enum
    {
        var field = typeof(T).GetField(enumValue.ToString());
        var fds = field!.GetCustomAttribute<DescriptionAttribute>(true);
        return fds!.Description;
    }
}
