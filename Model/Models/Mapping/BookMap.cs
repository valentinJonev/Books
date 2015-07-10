using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Name, t.PublishDate, t.Author });

            // Properties
            this.Property(t => t.Cover)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Books");
            this.Property(t => t.Cover).HasColumnName("Cover");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PublishDate).HasColumnName("PublishDate");
            this.Property(t => t.Author).HasColumnName("Author");
        }
    }
}
