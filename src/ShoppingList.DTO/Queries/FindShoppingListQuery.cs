﻿using MediatR;
using ShoppingList.DTO.Models;

namespace ShoppingList.DTO.Queries;

public record FindShoppingListQuery(int Id) : IRequest<ShoppingListDetails>;