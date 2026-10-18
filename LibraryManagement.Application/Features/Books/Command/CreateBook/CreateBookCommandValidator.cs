using FluentValidation;
using LibraryManagement.Application.Features.Books.Command.CreateBook;

public class CreateBookCommandValidator
    : AbstractValidator<CreateBookCommand>
{
  public CreateBookCommandValidator()
  {
    RuleFor(x => x.CreateBookDto)
        .NotNull()
        .DependentRules(() =>
        {
          RuleFor(x => x.CreateBookDto.Title)
                  .NotEmpty()
                  .MaximumLength(200);

          RuleFor(x => x.CreateBookDto.ISBN)
                  .NotEmpty()
                  .MaximumLength(50);

          RuleFor(x => x.CreateBookDto.PublishedDate)
                  .NotEmpty()
                  .WithMessage("Publication date is required.");

          RuleFor(x => x.CreateBookDto.AuthorId)
                  .GreaterThan(0)
                  .WithMessage("Please select an author.");
        });
  }
}
