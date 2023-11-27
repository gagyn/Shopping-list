namespace ShoppingList.DTO.Exceptions;
public class EntityNotFoundException(string name) : Exception($"{name} not found.");
public class EntityNotFoundException<T>() : EntityNotFoundException(typeof(T).Name);