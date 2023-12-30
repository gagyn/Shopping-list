namespace ShoppingList.DTO.Models.Users;
public record UserDetails
{
    public Guid Id { get; set; }
    public string? EmailAddress { get; set; }
    public UserRole Role { get; set; }
    public bool IsLocked { get; set; }

    public UserDetails()
    {
    }

    public UserDetails(Guid id, string? emailAddress, UserRole role, bool isLocked)
    {
        Id = id;
        EmailAddress = emailAddress;
        Role = role;
        IsLocked = isLocked;
    }
}
