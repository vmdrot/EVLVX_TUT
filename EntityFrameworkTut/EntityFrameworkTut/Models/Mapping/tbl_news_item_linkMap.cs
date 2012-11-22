using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkTut.Models.Mapping
{
    public class tbl_news_item_linkMap : EntityTypeConfiguration<tbl_news_item_link>
    {
        public tbl_news_item_linkMap()
        {
            // Primary Key
            this.HasKey(t => t.news_item_link_id);

            // Properties
            this.Property(t => t.link)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbl_news_item_link", "pr360srcnews");
            this.Property(t => t.news_item_link_id).HasColumnName("news_item_link_id");
            this.Property(t => t.news_item_id).HasColumnName("news_item_id");
            this.Property(t => t.portal_id).HasColumnName("portal_id");
            this.Property(t => t.link).HasColumnName("link");
        }
    }
}
