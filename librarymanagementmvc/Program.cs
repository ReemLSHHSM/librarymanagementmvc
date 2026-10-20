using LibraryManagement.Application;
using LibraryManagement.Infrastructure;

namespace librarymanagementmvc
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);


      builder.Services.AddApplication();
      builder.Services.AddInfrastructure(builder.Configuration);

      builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


      var app = builder.Build();

      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseRouting();

      app.UseAuthorization();

      app.MapStaticAssets();
      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Dashboard}/{action=Index}/{id?}")
          .WithStaticAssets();

      app.Run();
    }
  }
}
