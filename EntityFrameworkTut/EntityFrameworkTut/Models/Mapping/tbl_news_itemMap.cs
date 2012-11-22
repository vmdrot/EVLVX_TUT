using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkTut.Models.Mapping
{
    public class tbl_news_itemMap : EntityTypeConfiguration<tbl_news_item>
    {
        public tbl_news_itemMap()
        {
            // Primary Key
            this.HasKey(t => t.news_item_id);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbl_news_item", "pr360srcnews");
            this.Property(t => t.news_item_id).HasColumnName("news_item_id");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.body).HasColumnName("body");
        }
    }
}
