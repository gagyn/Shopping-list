using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Exceptions;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.QueryHandlers;
public class GetFavoriteRecipesQueryHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<GetFavoriteRecipesQuery, IReadOnlyCollection<RecipeShort>>
{
    public async Task<IReadOnlyCollection<RecipeShort>> Handle(GetFavoriteRecipesQuery request, CancellationToken cancellationToken)
    {
        var favoriteRecipes = await dbContext.Users
            .Where(x => x.Id == userAccessor.Id)
            .Select(x => x.FavoriteRecipes.Select(x => x.RecipeId))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException<ApplicationUserEntity>();

        var recipes = await dbContext.Recipes
            .Where(x => favoriteRecipes.Contains(x.Id))
            .Select(x => new RecipeShort(x.Id, x.Name, x.Description.ShorterDescription()))
            .ToListAsync(cancellationToken);

        return recipes ?? [];
    }
}
