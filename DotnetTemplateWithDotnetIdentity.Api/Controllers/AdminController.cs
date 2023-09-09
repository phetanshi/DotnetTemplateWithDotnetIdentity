using Application.Dtos;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : AppBaseController
    {
        private readonly IAppConfigService _appConfigService;
        public AdminController(IAppConfigService appConfigService, IConfiguration config, ILogger<AdminController> logger) : base(config, logger)
        {
            this._appConfigService = appConfigService;
        }

        [HttpGet("getAsync", Name = "GetAppConfig")]
        //[Authorize(Policy = AppPolicies.SUPPORT)]
        public async Task<IActionResult> GetAppConfig()
        {
            var appConfig = await _appConfigService.GetAsync();
            return OkDone(appConfig);
        }

        [HttpGet("appconfig/{configKey}", Name = "GetAppConfigByKey")]
        //[Authorize(Policy = AppPolicies.SUPPORT)]
        public async Task<IActionResult> GetAppConfigByKey(string configKey)
        {
            var appConfig = await _appConfigService.GetAsync(configKey);
            if (appConfig == null)
            {
                return ObjectNotFound();
            }
            return OkDone(appConfig);
        }

        [HttpPost("appconfig", Name = "createappconfig")]
        //[authorize(policy = apppolicies.admin)]
        public async Task<IActionResult> createappconfig(AppConfigCreateDto appconfig)
        {
            var appconfigdto = await _appConfigService.CreateAsync(appconfig);
            if (appconfigdto == null)
            {
                return InternalServerError();
            }
            return OkDone(appconfigdto);
        }
        [HttpPut("appconfig", Name = "UpdateAppConfig")]
        //[Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> UpdateAppConfig(AppConfigDto appConfig)
        {
            var appConfigDto = await _appConfigService.UpdateAsync(appConfig);
            if (appConfigDto == null)
            {
                return InternalServerError();
            }
            return OkDone(appConfigDto);
        }

        [HttpDelete("appconfig", Name = "DeleteAppConfig")]
        //[Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> DeleteAppConfig([FromBody] string configKeyg)
        {
            var isDeleted = await _appConfigService.DeleteAsync(configKeyg);
            if (isDeleted)
            {
                return InternalServerError();
            }
            return OkDone(isDeleted);
        }

        [HttpPost("appconfig/Search", Name = "SearchAppConfig")]
        public async Task<IActionResult> SearchAppConfig(SearchAppConfigDto searchstring)
        {
            var matchedConfigs = await _appConfigService.SearchAsync(searchstring);
            if (matchedConfigs == null)
            {
                return ObjectNotFound();
            }
            return OkDone(matchedConfigs);
        }
    }
}

