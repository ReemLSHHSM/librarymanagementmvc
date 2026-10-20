namespace LibraryManagement.Application.Features.Dashboard
{
  public class DashboardStatsDto
  {
    public int TotalBooks { get; set; }
    public int TotalAuthors { get; set; }
    public int TotalBorrowers { get; set; }
    public int ActiveLoans { get; set; }
  }
}
