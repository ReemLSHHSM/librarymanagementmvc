using LibraryManagement.Application.Features.Authors;
using LibraryManagement.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Application.Features.Books.DTOs;
using LibraryManagement.Application.Features.Books.Queries.GetAllBooks;
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
      var model = new BookFormViewModel();

      await LoadAuthorsAsync(model, cancellationToken);

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        BookFormViewModel model,
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

      return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Index(
    CancellationToken cancellationToken)
    {
      var books = await _sender.Send(
          new GetAllBooksQuery(),
          cancellationToken);

      var model = new GetAllBooksViewModel
      {
        Books = books.Select(b => new BookListItemViewModel
        {
          Id = b.Id,
          Title = b.Title,
          ISBN = b.ISBN,
          AuthorName = b.AuthorName,
          PublishedDate = b.PublishedDate,
          IsAvailable = b.IsAvailable
        }).ToList()
      };

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(
    int id,
    CancellationToken cancellationToken)
    {
      var book = await _sender.Send(
          new GetBookByIdQuery(id),
          cancellationToken);

      var model = new BookListItemViewModel
      {
        Id = book.Id,
        Title = book.Title,
        ISBN = book.ISBN,
        PublishedDate = book.PublishedDate,
        AuthorName = book.AuthorName,
        IsAvailable = book.IsAvailable
      };

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBook(
    int id,
    CancellationToken cancellationToken)
    {
      var book = await _sender.Send(
          new GetBookByIdQuery(id),
          cancellationToken);

      if (book is null)
      {
        return NotFound();
      }

      var model = new BookFormViewModel
      {
        Id = book.Id,
        Title = book.Title,
        ISBN = book.ISBN,
        PublishedDate = book.PublishedDate,
        AuthorId = book.AuthorId
      };

      await LoadAuthorsAsync(model, cancellationToken);

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> UpdateBook(
        BookFormViewModel model,
        CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        await LoadAuthorsAsync(model, cancellationToken);
        return View(model);
      }
      var dto = new UpdateBookDto
      {
        Id = (int)model.Id,
        Title = model.Title,
        ISBN = model.ISBN,
        PublishedDate = (DateOnly)model.PublishedDate,
        AuthorId = (int)model.AuthorId
      };

      var command = new UpdateBookCommand(dto);

      await _sender.Send(command, cancellationToken);

      TempData["SuccessMessage"] = "Book updated successfully.";

      return RedirectToAction(nameof(Index));
    }

    private async Task LoadAuthorsAsync(
        BookFormViewModel model,
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
