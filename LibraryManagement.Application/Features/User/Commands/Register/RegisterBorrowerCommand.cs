
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.User.Commands.Register
{
  public record RegisterBorrowerCommand(
    string Name,
    string Email,
    string Phone,
    string Password
) : IRequest<int>;

  public class RegisterBorrowerCommandHandler
      : IRequestHandler<RegisterBorrowerCommand, int>
  {

    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterBorrowerCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
      _context = context;
      _passwordHasher = passwordHasher;
    }

    public async Task<int> Handle(RegisterBorrowerCommand request, CancellationToken cancellationToken)
    {
      var emailExists = await _context.Users
          .AnyAsync(u => u.Email == request.Email, cancellationToken);

      if (emailExists)
      {
        throw new InvalidOperationException("Email already exists.");
      }

      var user = new LibraryManagement.Domain.Entities.User
      {
        Name = request.Name,
        Email = request.Email,
        PasswordHash = _passwordHasher.Hash(request.Password),
        Role = Domain.Enums.UserRole.Borrower
      };

      _context.Users.Add(user);


      var borrower = new Borrower
      {
        User = user,
        Phone = request.Phone
      };

      _context.Borrowers.Add(borrower);

      await _context.SaveChangesAsync(cancellationToken);

      return user.Id;
    }
  }
}
