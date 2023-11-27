namespace ShoppingList.DTO.Exceptions;
public class EntityNotFoundException(string name) : Exception($"{name} not found.");
