using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppMenuController : AppBaseController
    {
        private readonly IRoleMenuService _roleMenuService;

        public AppMenuController(IRoleMenuService roleMenuService, IConfiguration config, ILogger<AppMenuController> logger) : base(config, logger)
        {
            this._roleMenuService = roleMenuService;
        }

        [HttpGet]
        [Authorize(Policy = AppPolicies.DEFAULT)]
        public async Task<IActionResult> GetLoginUserMenu()
        {
            var result = await _roleMenuService.GetLoginUserMenuAsync();
            return OkDone(result);
        }
    }
}
