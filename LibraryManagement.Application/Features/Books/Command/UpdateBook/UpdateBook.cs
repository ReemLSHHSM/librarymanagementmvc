
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Command.UpdateBook
{
  public record UpdateBookCommand(UpdateBookDto UpdateBookDto) : IRequest;

  public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
  {
    private readonly IApplicationDbContext _context;

    public UpdateBookHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task Handle(
        UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
      var dto = request.UpdateBookDto;

      var book = await _context.Books.FindAsync(
          new object[] { dto.Id },
          cancellationToken);

      if (book is null)
      {
        throw new KeyNotFoundException("Book was not found.");
      }

      var author = await _context.Authors.FindAsync(
          new object[] { dto.AuthorId },
          cancellationToken);

      if (author is null)
      {
        throw new KeyNotFoundException("Author was not found.");
      }

      var isbn = dto.ISBN.Trim();

      var isbnExists = await _context.Books.AnyAsync(
          b => b.ISBN == isbn && b.Id != dto.Id,
          cancellationToken);

      if (isbnExists)
      {
        throw new InvalidOperationException(
            "A book with this ISBN already exists.");
      }

      book.Title = dto.Title.Trim();
      book.ISBN = isbn;
      book.PublishedDate = dto.PublishedDate;
      book.AuthorId = author.Id;

      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
