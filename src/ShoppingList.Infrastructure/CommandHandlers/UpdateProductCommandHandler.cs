using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUserAccessor userAccessor) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindOrThrow(request.ShoppingListId, request.ProductId, userAccessor.Id, cancellationToken);

        product.Update(request.Name, request.Description, request.Amount, request.IsCompleted);

        await productRepository.SaveChanges(cancellationToken);
    }
}

