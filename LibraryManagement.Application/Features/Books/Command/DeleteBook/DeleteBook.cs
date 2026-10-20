using LibraryManagement.Application.Common.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Command.DeleteBook
{

  public record DeleteBookCommand(int BookId) : IRequest;
  public class DeleteBook : IRequestHandler<DeleteBookCommand>
  {
    private readonly IApplicationDbContext _context;
    public DeleteBook(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
      var book = await _context.Books.FindAsync(new object[] { request.BookId }, cancellationToken);
      if (book is null)
      {
        throw new KeyNotFoundException("Book was not found.");
      }
      _context.Books.Remove(book);
      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
