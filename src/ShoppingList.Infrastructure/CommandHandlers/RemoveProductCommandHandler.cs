using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class RemoveProductCommandHandler(
    IProductRepository productRepository,
    IUserAccessor userAccessor) : IRequestHandler<RemoveProductCommand>
{
    public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.Remove(request.ShoppingListId, request.ProductId, userAccessor.Id);
        await productRepository.SaveChanges(cancellationToken);
    }
}
