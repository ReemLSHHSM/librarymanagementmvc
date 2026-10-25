
using LibraryManagement.Application.Features.User.Commands.Register;
using LibraryManagement.MVC.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers;

public class AccountController : Controller
{
  private readonly ISender _sender;

  public AccountController(ISender sender)
  {
    _sender = sender;
  }

  [HttpGet]
  public IActionResult Register()
  {
    return View(new RegisterViewModel());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Register(
      RegisterViewModel model,
      CancellationToken cancellationToken)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    var command = new RegisterBorrowerCommand(
        model.Name,
        model.Email,
        model.Phone,
        model.Password
    );

    try
    {
      await _sender.Send(command, cancellationToken);

      //return RedirectToAction("Login", "Account");
      TempData["SuccessMessage"] = "Account registered successfully!";
      return RedirectToAction("Index", "Dashboard");
    }
    catch (InvalidOperationException ex)
        when (ex.Message == "Email already exists.")
    {
      ModelState.AddModelError(
          nameof(model.Email),
          "This email address is already registered.");

      return View(model);
    }
  }
}
