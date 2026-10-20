using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.MVC.Models.Books
{
  public class BookFormViewModel
  {
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateOnly? PublishedDate { get; set; }

    public int? AuthorId { get; set; }

    public List<SelectListItem> Authors { get; set; } = new();

    public bool IsEditMode => Id.HasValue;
  }
}
