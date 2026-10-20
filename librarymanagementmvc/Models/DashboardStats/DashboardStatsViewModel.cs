namespace LibraryManagement.MVC.Models.DashboardStats
{
  public class DashboardStatsViewModel
  {
    public int TotalBooks { get; init; }
    public int TotalAuthors { get; init; }
    public int TotalBorrowers { get; init; }
    public int ActiveLoans { get; init; }
  }
}
