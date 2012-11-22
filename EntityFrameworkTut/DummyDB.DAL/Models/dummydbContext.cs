using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DummyDB.DAL.Models.Mapping;

namespace DummyDB.DAL.Models
{
    public class dummydbContext : DbContext
    {
        static dummydbContext()
        {
            Database.SetInitializer<dummydbContext>(null);
        }

		public dummydbContext()
            : base("Name=dummydbContext")
		{
		}

        public DbSet<tbl1> tbl1 { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new tbl1Map());
        }
    }
}
