using System.ComponentModel;

namespace ShoppingList.DTO.Models.Users;
public enum UserRole
{
    [Description("Basic User")]
    BasicUser = 1,

    [Description("Administrator")]
    Administrator
}