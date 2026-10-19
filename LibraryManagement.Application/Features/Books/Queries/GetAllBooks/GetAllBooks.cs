namespace LibraryManagement.Application.Features.Books.Queries.GetAllBooks
{
  public record GetAllBooksQuery : IRequest<List<BookDto>>
  {
  }
  public class GetAllBooks : IRequestHandel<GetAllBooksQuery, List<BookDto>>
  {

    private readonly IApplicationDbContext _context;
    public GetAllBooks(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
      var books = await _context.Books
          .Include(b => b.Author)
          .ToListAsync(cancellationToken);


      return books.Select(b => new BookDto
      {
        Id = b.Id,
        Title = b.Title,
        ISBN = b.ISBN,
        PublishedDate = b.PublishedDate,
        AuthorId = b.AuthorId,
        AuthorName = b.Author.Name,
        IsAvailable = !b.Loans.Any(l => l.ReturnedDate == null)
      }).ToList();


    }
  }
}
