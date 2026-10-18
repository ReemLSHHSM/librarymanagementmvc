using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Common.Interfaces
{
  public interface IApplicationDbContext
  {
    DbSet<Author> Authors { get; }
    DbSet<Book> Books { get; }
    DbSet<Borrower> Borrowers { get; }
    DbSet<Loan> Loans { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
  }
}
