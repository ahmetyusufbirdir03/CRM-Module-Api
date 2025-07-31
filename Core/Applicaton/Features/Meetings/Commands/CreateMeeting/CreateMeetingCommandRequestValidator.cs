using FluentValidation;

namespace Applicaton.Features.Meetings.Commands.CreateMeeting;

public class CreateMeetingCommandRequestValidator : AbstractValidator<CreateMeetingCommandRequest>
{
    public CreateMeetingCommandRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.")
            .GreaterThan(x => x.StartDate).WithMessage("Toplantı bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");

        RuleFor(x => x.TypeId)
            .GreaterThan(0).WithMessage("Geçerli bir tür giriniz.")
            .LessThanOrEqualTo(7);

        RuleFor(x => x.StateId)
            .GreaterThan(0).WithMessage("Geçerli bir durum giriniz.")
            .LessThanOrEqualTo(6);

        RuleFor(x => x.FormatId)
            .GreaterThan(0).WithMessage("Geçerli bir format giriniz.")
            .LessThanOrEqualTo(5);
    }
}