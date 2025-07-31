using FluentValidation;
using System.Text.RegularExpressions;

namespace Applicaton.Features.Auth.Commands.Revoke;

public class RevokeCommandValidator : AbstractValidator<RevokeCommandRequest>
{
    public RevokeCommandValidator()
    {
        RuleFor(x => x.PhoneNumberOrEmail)
            .NotEmpty().WithMessage("E-posta veya telefon numarası boş olamaz.")
            .Must(BeValidEmailOrPhone).WithMessage("Geçerli bir e-posta veya telefon numarası giriniz.");
    }

    private bool BeValidEmailOrPhone(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Email format kontrolü
        bool isEmail = Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        // Telefon numarası formatı kontrolü
        bool isPhone = Regex.IsMatch(input, @"^0\d{10}$");

        return isEmail || isPhone;
    }
}