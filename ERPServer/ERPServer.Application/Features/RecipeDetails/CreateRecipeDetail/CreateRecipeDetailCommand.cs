using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
public sealed record CreateRecipeDetailCommand(
    Guid RecipeId,
    Guid ProductId,
    decimal Quantity) : IRequest<Result<string>>;
