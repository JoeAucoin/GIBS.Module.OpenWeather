using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.OpenWeather.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.OpenWeather.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class OpenWeatherController : ModuleControllerBase
    {
        private readonly IOpenWeatherService _OpenWeatherService;

        public OpenWeatherController(IOpenWeatherService OpenWeatherService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _OpenWeatherService = OpenWeatherService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.OpenWeather>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _OpenWeatherService.GetOpenWeathersAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.OpenWeather> Get(int id, int moduleid)
        {
            Models.OpenWeather OpenWeather = await _OpenWeatherService.GetOpenWeatherAsync(id, moduleid);
            if (OpenWeather != null && IsAuthorizedEntityId(EntityNames.Module, OpenWeather.ModuleId))
            {
                return OpenWeather;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Get Attempt {OpenWeatherId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.OpenWeather> Post([FromBody] Models.OpenWeather OpenWeather)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, OpenWeather.ModuleId))
            {
                OpenWeather = await _OpenWeatherService.AddOpenWeatherAsync(OpenWeather);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Post Attempt {OpenWeather}", OpenWeather);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                OpenWeather = null;
            }
            return OpenWeather;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.OpenWeather> Put(int id, [FromBody] Models.OpenWeather OpenWeather)
        {
            if (ModelState.IsValid && OpenWeather.OpenWeatherId == id && IsAuthorizedEntityId(EntityNames.Module, OpenWeather.ModuleId))
            {
                OpenWeather = await _OpenWeatherService.UpdateOpenWeatherAsync(OpenWeather);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Put Attempt {OpenWeather}", OpenWeather);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                OpenWeather = null;
            }
            return OpenWeather;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.OpenWeather OpenWeather = await _OpenWeatherService.GetOpenWeatherAsync(id, moduleid);
            if (OpenWeather != null && IsAuthorizedEntityId(EntityNames.Module, OpenWeather.ModuleId))
            {
                await _OpenWeatherService.DeleteOpenWeatherAsync(id, OpenWeather.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized OpenWeather Delete Attempt {OpenWeatherId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
