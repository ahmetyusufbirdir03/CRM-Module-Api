using FluentValidation;

namespace Applicaton.Features.User.Commands.DeleteUser;

public class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommandRequest>
{
    public DeleteUserByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}