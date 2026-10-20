

using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Configurations
{
  public class BookConfiguration : IEntityTypeConfiguration<Book>
  {
    public void Configure(EntityTypeBuilder<Book> builder)
    {
      builder.HasKey(b => b.Id);

      builder.Property(b => b.Title)
          .IsRequired()
          .HasMaxLength(200);

      builder.Property(b => b.ISBN)
          .IsRequired()
          .HasMaxLength(50);

      builder.HasIndex(b => b.ISBN)
          .IsUnique();

      builder.Property(b => b.PublishedDate)
          .IsRequired();

      builder.Property(b => b.IsAvailable)
          .IsRequired();

      builder.HasOne(b => b.Author)
     .WithMany(a => a.Books)
     .HasForeignKey(b => b.AuthorId)
     .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
