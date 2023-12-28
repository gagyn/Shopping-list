using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUserAccessor userAccessor) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindOrThrow(request.ShoppingListId, request.ProductId, userAccessor.Id, cancellationToken);

        product.Update(request.Name, request.Description, request.Amount, request.IsCompleted, request.Unit.Parse<UnitEntity>());

        await productRepository.SaveChanges(cancellationToken);
    }
}