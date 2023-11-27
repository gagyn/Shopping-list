using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database.Configurations;

namespace ShoppingList.Infrastructure.QueryHandlers;
public class GetShoppingListsQueryHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<GetShoppingListsQuery, IReadOnlyCollection<DTO.Models.ShoppingList>>
{
    public async Task<IReadOnlyCollection<DTO.Models.ShoppingList>> Handle(GetShoppingListsQuery request, CancellationToken cancellationToken)
        => await dbContext.ShoppingLists
            .Where(x => x.OwnedByUserId == userAccessor.Id)
            .Select(x => new DTO.Models.ShoppingList(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}
