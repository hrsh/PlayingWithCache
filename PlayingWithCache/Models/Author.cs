using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayingWithCache.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }

    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(256);
        }
    }
}
