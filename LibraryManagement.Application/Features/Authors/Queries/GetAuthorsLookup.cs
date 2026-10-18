using LibraryManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Authors
{
  public record AuthorLookupDto(int Id, string Name);

  public record GetAuthorsLookupQuery
      : IRequest<List<AuthorLookupDto>>;

  public class GetAuthorsLookupHandler
      : IRequestHandler<GetAuthorsLookupQuery, List<AuthorLookupDto>>
  {
    private readonly IApplicationDbContext _context;

    public GetAuthorsLookupHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<AuthorLookupDto>> Handle(
        GetAuthorsLookupQuery request,
        CancellationToken cancellationToken)
    {
      return await _context.Authors
          .AsNoTracking()
          .OrderBy(author => author.Name)
          .Select(author => new AuthorLookupDto(
              author.Id,
              author.Name))
          .ToListAsync(cancellationToken);
    }
  }
}
