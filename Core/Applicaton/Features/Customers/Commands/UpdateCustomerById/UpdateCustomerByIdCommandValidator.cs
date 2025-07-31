using FluentValidation;

namespace Applicaton.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerByIdCommandValidator : AbstractValidator<UpdateCustomerByIdCommandRequest>
{
    public UpdateCustomerByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(11)
            .Matches(@"^0\d{10}$");

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.TypeId)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(4);

        RuleFor(x => x.StateId)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(4); 

    }
}