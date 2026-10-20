
using LibraryManagement.Application.Features.Dashboard.GetDashboardStats;
using LibraryManagement.MVC.Models.DashboardStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers
{
  public class DashboardController : Controller
  {
    private readonly ISender _sender;

    public DashboardController(ISender sender)
    {
      _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
      var stats = await _sender.Send(
          new GetDashboardStatsQuery(),
          cancellationToken);

      var model = new DashboardStatsViewModel
      {
        TotalBooks = stats.TotalBooks,
        TotalAuthors = stats.TotalAuthors,
        TotalBorrowers = stats.TotalBorrowers,
        ActiveLoans = stats.ActiveLoans
      };

      return View(model);
    }
  }
}
