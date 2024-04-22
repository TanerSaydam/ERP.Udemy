using FluentValidation;

namespace ERPServer.Application.Features.Customers.UpdateCustomer;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.TaxNumber).MinimumLength(10).MaximumLength(11);
    }
}
