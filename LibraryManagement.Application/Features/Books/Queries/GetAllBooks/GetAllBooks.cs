using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Queries.GetAllBooks
{
  public record GetAllBooksQuery : IRequest<List<BookDto>>
  {
  }
  public class GetAllBooks : IRequestHandler<GetAllBooksQuery, List<BookDto>>
  {

    private readonly IApplicationDbContext _context;
    public GetAllBooks(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<List<BookDto>> Handle(
     GetAllBooksQuery request,
     CancellationToken cancellationToken)
    {
      return await _context.Books
          .AsNoTracking()
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
          .ToListAsync(cancellationToken);
    }
  }
}
