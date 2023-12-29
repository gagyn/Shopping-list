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
            .Where(x => string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name.ToLower()))
            .Select(x => new RecipeShort(
                x.Id,
                x.Name,
                x.Description.Length > 50 ? x.Description.Substring(0, 50).Trim() + "..." : x.Description))
            .ToListAsync(cancellationToken);
}