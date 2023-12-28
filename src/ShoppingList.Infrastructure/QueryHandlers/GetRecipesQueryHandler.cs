using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.QueryHandlers;

public class GetRecipesQueryHandler(
    ShoppingListContext dbContext) : IRequestHandler<GetRecipesQuery, IReadOnlyCollection<RecipeShort>>
{
    public async Task<IReadOnlyCollection<RecipeShort>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        => await dbContext.Recipes
            .Where(x => string.IsNullOrEmpty(request.Name) || x.Name.StartsWith(request.Name))
            .Select(x => new RecipeShort(x.Id, x.Name, x.Description.Take(50).ToString()!))
            .ToListAsync(cancellationToken);
}