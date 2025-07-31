using FluentValidation;
using MediatR;

namespace Applicaton.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");
        //.Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir")
        //.Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir")
        //.Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir")

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Parola boş olamaz!")
            .MinimumLength(6)
            .Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor");

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
