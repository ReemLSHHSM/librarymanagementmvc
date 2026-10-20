using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
  public class Borrower : BaseEntity
  {
    public string Phone { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
  }
}
