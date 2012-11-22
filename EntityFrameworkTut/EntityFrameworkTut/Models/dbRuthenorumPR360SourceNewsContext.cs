using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EntityFrameworkTut.Models.Mapping;

namespace EntityFrameworkTut.Models
{
    public class dbRuthenorumPR360SourceNewsContext : DbContext
    {
        static dbRuthenorumPR360SourceNewsContext()
        {
            Database.SetInitializer<dbRuthenorumPR360SourceNewsContext>(null);
        }

		public dbRuthenorumPR360SourceNewsContext()
			: base("Name=dbRuthenorumPR360SourceNewsContext")
		{
		}

        public DbSet<tbl_news_item> tbl_news_item { get; set; }
        public DbSet<tbl_news_item_link> tbl_news_item_link { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new tbl_news_itemMap());
            modelBuilder.Configurations.Add(new tbl_news_item_linkMap());
        }
    }
}
