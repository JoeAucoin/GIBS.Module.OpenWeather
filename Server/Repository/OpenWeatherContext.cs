using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.OpenWeather.Repository
{
    public class OpenWeatherContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.OpenWeather> OpenWeather { get; set; }

        public OpenWeatherContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.OpenWeather>().ToTable(ActiveDatabase.RewriteName("GIBSOpenWeather"));
        }
    }
}
