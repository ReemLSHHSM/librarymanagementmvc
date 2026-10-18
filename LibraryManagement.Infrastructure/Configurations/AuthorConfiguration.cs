

using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Configurations
{
  public class AuthorConfiguration : IEntityTypeConfiguration<Author>
  {
    public void Configure(EntityTypeBuilder<Author> builder)
    {
      builder.HasKey(a => a.Id);

      builder.Property(a => a.Name)
          .IsRequired()
          .HasMaxLength(100);

      builder.Property(a => a.Bio)
          .HasMaxLength(1000);
    }
  }
}
