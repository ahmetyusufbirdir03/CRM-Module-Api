using FluentValidation;

namespace Applicaton.Features.User.Commands.UpdateUserById;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş olamaz")
            .MaximumLength(15);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş olamaz");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Departman boş olamaz")
            .MaximumLength(15);

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Ünvan boş olamaz")
            .MaximumLength(15);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz")
            .Matches(@"^0\d{10}$").WithMessage("Geçerli bir telefon numarası giriniz (örn: 05551112233)");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir");
    }
}