using FluentValidation;

namespace LibraryManagement.Application.Features.Books.Command.UpdateBook
{
  public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
  {
    public UpdateBookCommandValidator()
    {
      RuleFor(x => x.UpdateBookDto)
          .NotNull()
          .DependentRules(() =>
          {
            RuleFor(x => x.UpdateBookDto.Id)
                    .GreaterThan(0)
                    .WithMessage("Please provide a valid book ID.");
            RuleFor(x => x.UpdateBookDto.Title)
                    .NotEmpty()
                    .MaximumLength(200);
            RuleFor(x => x.UpdateBookDto.ISBN)
                    .NotEmpty()
                    .MaximumLength(50);
            RuleFor(x => x.UpdateBookDto.PublishedDate)
                    .NotEmpty()
                    .WithMessage("Publication date is required.");
            RuleFor(x => x.UpdateBookDto.AuthorId)
                    .GreaterThan(0)
                    .WithMessage("Please select an author.");
          });
    }

  }
}
