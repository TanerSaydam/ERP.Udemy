using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.GetAllCustomer;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers;

public sealed class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
