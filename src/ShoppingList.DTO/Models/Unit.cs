using System.ComponentModel;

namespace ShoppingList.DTO.Models;
public enum Unit
{
    [Description("szt")]
    Quantity = 1,

    [Description("kg")]
    Kilograms,

    [Description("g")]
    Grams,

    [Description("l")]
    Liter
}