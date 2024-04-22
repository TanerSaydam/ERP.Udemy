using ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ERPServer.Application.Features.RecipeDetails.DeleteRecipeDetailById;
using ERPServer.Application.Features.RecipeDetails.GetRecipeByIdWithDetails;
using ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers;

public sealed class RecipeDetailsController : ApiController
{
    public RecipeDetailsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetRecipeByIdWithDetails(GetRecipeByIdWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteRecipeDetailByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
