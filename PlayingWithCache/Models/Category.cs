using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace PlayingWithCache.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Outline { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(a => a.Outline).IsRequired(false);
        }
    }
}
