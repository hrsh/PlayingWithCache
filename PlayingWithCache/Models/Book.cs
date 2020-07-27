using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PlayingWithCache.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }

    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(a => a.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Category)
                .WithMany(a => a.Books)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired();

            builder.HasOne(a => a.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(a => a.AuthorId)
                .IsRequired();
        }
    }
}
