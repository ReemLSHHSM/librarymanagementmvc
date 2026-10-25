using LibraryManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
  private readonly PasswordHasher<object> _hasher = new();

  public string Hash(string password)
  {
    return _hasher.HashPassword(new object(), password);
  }

  public bool Verify(string password, string passwordHash)
  {
    var result = _hasher.VerifyHashedPassword(
        new object(),
        passwordHash,
        password
    );

    return result != PasswordVerificationResult.Failed;
  }
}
