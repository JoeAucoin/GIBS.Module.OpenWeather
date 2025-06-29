using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.OpenWeather.Repository;

namespace GIBS.Module.OpenWeather.Services
{
    public class ServerOpenWeatherService : IOpenWeatherService
    {
        private readonly IOpenWeatherRepository _OpenWeatherRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerOpenWeatherService(IOpenWeatherRepository OpenWeatherRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _OpenWeatherRepository = OpenWeatherRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.OpenWeather>> GetOpenWeathersAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_OpenWeatherRepository.GetOpenWeathers(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.OpenWeather> GetOpenWeatherAsync(int OpenWeatherId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_OpenWeatherRepository.GetOpenWeather(OpenWeatherId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Get Attempt {OpenWeatherId} {ModuleId}", OpenWeatherId, ModuleId);
                return null;
            }
        }

        public Task<Models.OpenWeather> AddOpenWeatherAsync(Models.OpenWeather OpenWeather)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, OpenWeather.ModuleId, PermissionNames.Edit))
            {
                OpenWeather = _OpenWeatherRepository.AddOpenWeather(OpenWeather);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "OpenWeather Added {OpenWeather}", OpenWeather);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Add Attempt {OpenWeather}", OpenWeather);
                OpenWeather = null;
            }
            return Task.FromResult(OpenWeather);
        }

        public Task<Models.OpenWeather> UpdateOpenWeatherAsync(Models.OpenWeather OpenWeather)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, OpenWeather.ModuleId, PermissionNames.Edit))
            {
                OpenWeather = _OpenWeatherRepository.UpdateOpenWeather(OpenWeather);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "OpenWeather Updated {OpenWeather}", OpenWeather);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Update Attempt {OpenWeather}", OpenWeather);
                OpenWeather = null;
            }
            return Task.FromResult(OpenWeather);
        }

        public Task DeleteOpenWeatherAsync(int OpenWeatherId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _OpenWeatherRepository.DeleteOpenWeather(OpenWeatherId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "OpenWeather Deleted {OpenWeatherId}", OpenWeatherId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Delete Attempt {OpenWeatherId} {ModuleId}", OpenWeatherId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
