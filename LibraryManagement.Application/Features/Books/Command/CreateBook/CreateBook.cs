using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using LibraryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Command.CreateBook
{
  public record CreateBookCommand(CreateBookDto CreateBookDto)
      : IRequest<BookDto>;

  public class CreateBookHandler
      : IRequestHandler<CreateBookCommand, BookDto>
  {
    private readonly IApplicationDbContext _context;

    public CreateBookHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<BookDto> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
      var dto = request.CreateBookDto;

      var author = await _context.Authors.FindAsync(
          new object[] { dto.AuthorId },
          cancellationToken);

      if (author is null)
      {
        throw new KeyNotFoundException("Author was not found.");
      }

      var isbn = dto.ISBN.Trim();

      var isbnExists = await _context.Books.AnyAsync(
          b => b.ISBN == isbn,
          cancellationToken);

      if (isbnExists)
      {
        throw new InvalidOperationException(
            "A book with this ISBN already exists.");
      }

      var book = new Book
      {
        Title = dto.Title.Trim(),
        ISBN = isbn,
        PublishedDate = dto.PublishedDate,
        AuthorId = author.Id
      };

      _context.Books.Add(book);

      await _context.SaveChangesAsync(cancellationToken);

      return new BookDto
      {
        Id = book.Id,
        Title = book.Title,
        ISBN = book.ISBN,
        PublishedDate = book.PublishedDate,
        AuthorId = book.AuthorId,
        AuthorName = author.Name,
        IsAvailable = book.IsAvailable
      };
    }
  }
}
