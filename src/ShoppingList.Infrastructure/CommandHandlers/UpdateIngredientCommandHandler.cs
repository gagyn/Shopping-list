using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.DTO.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateIngredientCommandHandler(
    IIngredientRepository ingredientRepository) : IRequestHandler<UpdateIngredientCommand>
{
    public async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientRepository.FindOrThrow(request.RecipeId, request.IngredientId, cancellationToken);

        ingredient.Update(request.Name, request.Description, request.Amount, request.Unit.Parse<UnitEntity>());

        await ingredientRepository.SaveChanges(cancellationToken);
    }
}
