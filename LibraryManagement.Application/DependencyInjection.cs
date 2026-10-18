using FluentValidation;
using LibraryManagement.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
      var assembly = typeof(DependencyInjection).Assembly;

      services.AddValidatorsFromAssembly(assembly);

      services.AddMediatR(configuration =>
      {
        configuration.RegisterServicesFromAssembly(assembly);

        configuration.AddOpenBehavior(
            typeof(ValidationBehavior<,>));
      });

      return services;
    }
  }
}
