﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.QueryHandlers;
public class GetShoppingListsQueryHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<GetShoppingListsQuery, IReadOnlyCollection<DTO.Models.ShoppingListShort>>
{
    public async Task<IReadOnlyCollection<DTO.Models.ShoppingListShort>> Handle(GetShoppingListsQuery request, CancellationToken cancellationToken)
        => await dbContext.ShoppingLists
            .Where(x => x.OwnedByUserId == userAccessor.Id
                && (string.IsNullOrEmpty(request.Name) || x.Name.StartsWith(request.Name)))
            .Select(x => new DTO.Models.ShoppingListShort(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}
