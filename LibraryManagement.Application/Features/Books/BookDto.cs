namespace LibraryManagement.Application.Features.Books.DTOs
{
  public class CreateBookDto
  {
    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public int AuthorId { get; set; }
  }

  public class UpdateBookDto
  {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public int AuthorId { get; set; }
  }

  public class BookDto
  {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
  }
}
