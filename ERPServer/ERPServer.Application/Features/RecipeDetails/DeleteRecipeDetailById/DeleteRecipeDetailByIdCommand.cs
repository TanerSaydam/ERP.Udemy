using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.DeleteRecipeDetailById;
public sealed record DeleteRecipeDetailByIdCommand(
    Guid Id) : IRequest<Result<string>>;
