using FluentValidation;

namespace Applicaton.Features.Customers.Queries.GetCustomerByPhoneNumber;

public class GetCustomerByPhoneNumberQueryValidator : AbstractValidator<GetCustomerByPhoneNumberQueryRequest>
{
    public GetCustomerByPhoneNumberQueryValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(11)
            .Matches(@"^0\d{10}$");
    }
}
