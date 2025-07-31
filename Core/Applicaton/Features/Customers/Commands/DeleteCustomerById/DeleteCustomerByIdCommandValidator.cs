using FluentValidation;

namespace Applicaton.Features.Customers.Commands.DeleteCustomerById;

public class DeleteCustomerByIdCommandValidator : AbstractValidator<DeleteCustomerByIdCommandRequest>
{
    public DeleteCustomerByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}
