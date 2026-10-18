using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
  public class Book : BaseEntity
  {
    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public bool IsAvailable { get; set; } = true;


    public int AuthorId { get; set; }

    public Author Author { get; set; } = null!;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
  }
}
