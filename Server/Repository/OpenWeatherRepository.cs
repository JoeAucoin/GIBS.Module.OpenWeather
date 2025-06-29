using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace GIBS.Module.OpenWeather.Repository
{
    public class OpenWeatherRepository : IOpenWeatherRepository, ITransientService
    {
        private readonly IDbContextFactory<OpenWeatherContext> _factory;

        public OpenWeatherRepository(IDbContextFactory<OpenWeatherContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.OpenWeather> GetOpenWeathers(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.OpenWeather.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.OpenWeather GetOpenWeather(int OpenWeatherId)
        {
            return GetOpenWeather(OpenWeatherId, true);
        }

        public Models.OpenWeather GetOpenWeather(int OpenWeatherId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.OpenWeather.Find(OpenWeatherId);
            }
            else
            {
                return db.OpenWeather.AsNoTracking().FirstOrDefault(item => item.OpenWeatherId == OpenWeatherId);
            }
        }

        public Models.OpenWeather AddOpenWeather(Models.OpenWeather OpenWeather)
        {
            using var db = _factory.CreateDbContext();
            db.OpenWeather.Add(OpenWeather);
            db.SaveChanges();
            return OpenWeather;
        }

        public Models.OpenWeather UpdateOpenWeather(Models.OpenWeather OpenWeather)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(OpenWeather).State = EntityState.Modified;
            db.SaveChanges();
            return OpenWeather;
        }

        public void DeleteOpenWeather(int OpenWeatherId)
        {
            using var db = _factory.CreateDbContext();
            Models.OpenWeather OpenWeather = db.OpenWeather.Find(OpenWeatherId);
            db.OpenWeather.Remove(OpenWeather);
            db.SaveChanges();
        }
    }
}
