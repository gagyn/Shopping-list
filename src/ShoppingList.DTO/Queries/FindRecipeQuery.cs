using MediatR;
using ShoppingList.DTO.Models;

namespace ShoppingList.DTO.Queries;

public record FindRecipeQuery(int Id) : IRequest<RecipeDetails>;