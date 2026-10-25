using FluentValidation;
using LibraryManagement.Application.Features.User.Commands.Register;

namespace LibraryManagement.Application.Features.Authentication.Register;

public sealed class RegisterBorrowerCommandValidator
    : AbstractValidator<RegisterBorrowerCommand>
{
  public RegisterBorrowerCommandValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(100);

    RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .MaximumLength(200);

    RuleFor(x => x.Phone)
    .NotEmpty()
    .Matches(@"^\+?\d{10,15}$")
    .WithMessage("Phone number must contain 10 to 15 digits, optionally starting with +.")
    .MaximumLength(30);

    RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .MaximumLength(128);
  }
}
