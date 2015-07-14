using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.BookId);

            // Properties
            this.Property(t => t.BookId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Books");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Cover).HasColumnName("Cover");
            this.Property(t => t.PublishDate).HasColumnName("PublishDate");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");
        }
    }
}
