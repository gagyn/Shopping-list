﻿using ShoppingList.Domain.Base;
using ShoppingList.Domain.Exceptions;

namespace ShoppingList.Domain.ShoppingList;
public class ShoppingListEntity : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<ProductEntity> Products { get; private set; } = [];
    public Guid OwnedByUserId { get; private set; }

    private ShoppingListEntity()
    {
    }

    private ShoppingListEntity(string name, Guid ownedByUserId, string createdBy) : base(createdBy)
    {
        Name = name;
        OwnedByUserId = ownedByUserId;
    }

    public static ShoppingListEntity Create(string name, Guid ownedByUserId, string createdBy)
        => new(name, ownedByUserId, createdBy);

    public ProductEntity AddProduct(string productName, string productDescription, decimal amount, string modifiedBy, UnitEntity unit)
    {
        var product = ProductEntity.Create(Id, productName, productDescription, amount, unit);
        Products.Add(product);
        SetModified(modifiedBy);
        return product;
    }

    public void UpdateName(string name, string modifiedBy)
    {
        Name = name;
        SetModified(modifiedBy);
    }

    public void RemoveProduct(int productId, string modifiedBy)
    {
        var product = Products.FirstOrDefault(x => x.Id == productId)
            ?? throw new EntityNotFoundException<ProductEntity>();

        Products.Remove(product);
        SetModified(modifiedBy);
    }
}
