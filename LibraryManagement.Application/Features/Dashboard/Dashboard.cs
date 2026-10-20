
using LibraryManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Dashboard.GetDashboardStats
{
  public record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

  public class GetDashboardStatsHandler
      : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
  {
    private readonly IApplicationDbContext _context;

    public GetDashboardStatsHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<DashboardStatsDto> Handle(
        GetDashboardStatsQuery request,
        CancellationToken cancellationToken)
    {
      var totalBooks = await _context.Books
          .CountAsync(cancellationToken);

      var totalAuthors = await _context.Authors
          .CountAsync(cancellationToken);

      var totalBorrowers = await _context.Borrowers
          .CountAsync(cancellationToken);

      var activeLoans = await _context.Loans
          .CountAsync(
              loan => loan.ReturnDate == null,
              cancellationToken);

      return new DashboardStatsDto
      {
        TotalBooks = totalBooks,
        TotalAuthors = totalAuthors,
        TotalBorrowers = totalBorrowers,
        ActiveLoans = activeLoans
      };
    }
  }
}
