using Application.Dtos;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = AppPolicies.SUPPORT)]
    public class LoggerController : AppBaseController
    {
        private readonly IAppLogService _appLogService;
        public LoggerController(IAppLogService appLogService, IConfiguration config, ILogger<LoggerController> logger) : base(config, logger)
        {
            this._appLogService = appLogService;
        }

        [HttpGet(Name = "GetActivityLogs")]
        public async Task<IActionResult> GetActivityLogs([FromQuery] SearchCriteria searchCriteria)
        {
            var result = await _appLogService.GetActivityLogsAsync(searchCriteria);
            return OkDone(result);
        }

        [HttpGet("error", Name = "GetErrorLogs")]
        public async Task<IActionResult> GetErrorLogs([FromQuery] SearchCriteria searchCriteria)
        {
            var result = await _appLogService.GetErrorLogsAsync(searchCriteria);
            return OkDone(result);
        }
    }
}
