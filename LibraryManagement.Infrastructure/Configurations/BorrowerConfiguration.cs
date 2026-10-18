using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Configurations
{
  public class BorrowerConfiguration
      : IEntityTypeConfiguration<Borrower>
  {
    public void Configure(
        EntityTypeBuilder<Borrower> builder)
    {
      builder.HasKey(b => b.Id);

      builder.Property(b => b.Phone)
          .IsRequired()
          .HasMaxLength(30);

      builder.HasOne(b => b.User)
          .WithOne(u => u.Borrower)
          .HasForeignKey<Borrower>(b => b.UserId);

      builder.HasMany(b => b.Loans)
          .WithOne(l => l.Borrower)
          .HasForeignKey(l => l.BorrowerId);
    }
  }
}
