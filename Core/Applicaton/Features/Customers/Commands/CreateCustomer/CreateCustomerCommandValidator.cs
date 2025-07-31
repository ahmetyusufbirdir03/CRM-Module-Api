using FluentValidation;

namespace Applicaton.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommandRequest>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(11)
            .Matches(@"^0\d{10}$");

        RuleFor(x => x.StateId)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(4);

        RuleFor(x => x.TypeId)
            .GreaterThan(0)
            .LessThanOrEqualTo(4)
            .NotEmpty();
    }
}
