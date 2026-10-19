namespace LibraryManagement.MVC.Models.Books
{
  public class BookListItemViewModel
  {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public DateOnly PublishedDate { get; set; }

    public bool IsAvailable { get; set; }
  }

  public class GetAllBooksViewModel
  {
    public List<BookListItemViewModel> Books { get; set; } = new();
  }
}
