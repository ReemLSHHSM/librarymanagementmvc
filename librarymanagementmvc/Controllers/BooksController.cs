using LibraryManagement.Application.Features.Authors;
using LibraryManagement.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Application.Features.Books.DTOs;
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
      var model = new CreateBookViewModel();

      await LoadAuthorsAsync(model, cancellationToken);

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateBookViewModel model,
        CancellationToken cancellationToken)
    {
      if (string.IsNullOrWhiteSpace(model.Title))
      {
        ModelState.AddModelError(
            nameof(model.Title),
            "Book title is required.");
      }

      if (string.IsNullOrWhiteSpace(model.ISBN))
      {
        ModelState.AddModelError(
            nameof(model.ISBN),
            "ISBN is required.");
      }

      if (!model.PublishedDate.HasValue)
      {
        ModelState.AddModelError(
            nameof(model.PublishedDate),
            "Published date is required.");
      }

      if (!model.AuthorId.HasValue || model.AuthorId.Value <= 0)
      {
        ModelState.AddModelError(
            nameof(model.AuthorId),
            "Please select an author.");
      }

      if (!ModelState.IsValid)
      {
        await LoadAuthorsAsync(model, cancellationToken);

        return View(model);
      }

      var dto = new CreateBookDto
      {
        Title = model.Title,
        ISBN = model.ISBN,
        PublishedDate = model.PublishedDate!.Value,
        AuthorId = model.AuthorId!.Value
      };

      var command = new CreateBookCommand(dto);

      await _sender.Send(command, cancellationToken);

      TempData["SuccessMessage"] = "Book created successfully.";

      return RedirectToAction(nameof(Create));
    }

    private async Task LoadAuthorsAsync(
        CreateBookViewModel model,
        CancellationToken cancellationToken)
    {
      var authors = await _sender.Send(
          new GetAuthorsLookupQuery(),
          cancellationToken);

      model.Authors = authors.Select(author => new SelectListItem
      {
        Value = author.Id.ToString(),
        Text = author.Name
      }).ToList();
    }
  }
}
