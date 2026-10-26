

using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Common.Interfaces
{
  public interface IJwtTokenGenerator
  {

    string GenerateToken(User user);
  }
}
