using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DummyDBRemote.Models.Mapping
{
    public class tbl1Map : EntityTypeConfiguration<tbl1>
    {
        public tbl1Map()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.nm)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.descr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbl1");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.nm).HasColumnName("nm");
            this.Property(t => t.is_valid).HasColumnName("is_valid");
            this.Property(t => t.price).HasColumnName("price");
            this.Property(t => t.descr).HasColumnName("descr");
            this.Property(t => t.cnt).HasColumnName("cnt");
        }
    }
}
