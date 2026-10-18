using LibraryManagement.Application.Features.Authors;
using LibraryManagement.MVC.Models.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.MVC.Controllers
{
  public class BooksController : Controller
  {
    private readonly ISender _sender;

    public BooksController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
      var authors = await _sender.Send(
          new GetAuthorsLookupQuery(),
          cancellationToken);

      var model = new CreateBookViewModel
      {
        Authors = authors.Select(author => new SelectListItem
        {
          Value = author.Id.ToString(),
          Text = author.Name
        }).ToList()
      };

      return View(model);
    }
  }
}
