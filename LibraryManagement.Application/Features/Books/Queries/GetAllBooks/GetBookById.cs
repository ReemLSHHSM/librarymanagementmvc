using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Queries.GetAllBooks
{
  public record GetBookByIdQuery(int Id) : IRequest<BookDto>
  {
  }
  public class GetBookById : IRequestHandler<GetBookByIdQuery, BookDto>
  {
    private readonly IApplicationDbContext _context;
    public GetBookById(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<BookDto> Handle(
     GetBookByIdQuery request,
     CancellationToken cancellationToken)
    {
      var book = await _context.Books
          .AsNoTracking()
          .Where(b => b.Id == request.Id)
          .Select(b => new BookDto
          {
            Id = b.Id,
            Title = b.Title,
            ISBN = b.ISBN,
            PublishedDate = b.PublishedDate,
            AuthorId = b.AuthorId,
            AuthorName = b.Author.Name,
            IsAvailable = !b.Loans.Any(
                  l => l.ReturnDate == null)
          })
          .FirstOrDefaultAsync(cancellationToken);
      if (book == null)
      {
        throw new KeyNotFoundException($"Book with ID {request.Id} not found.");
      }
      return book;
    }
  }
}
