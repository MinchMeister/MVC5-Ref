using MVC5_Ref.DAL.Models;
using System.Data.Entity;

namespace MVC5_Ref.DAL.Repositories
{
    public class SiteContext : DbContext
    {
        public SiteContext() : base("name = SiteDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<PersonInfo> PersonInfoes { get; set; }
    }
}
