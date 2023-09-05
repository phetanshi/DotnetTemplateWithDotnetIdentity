using Application.Dtos;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : AppBaseController
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService, IConfiguration config, ILogger<UserRoleController> logger) : base(config, logger)
        {
            this._userRoleService = userRoleService;
        }

        [HttpGet("{userId:int}", Name = "GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var result = await _userRoleService.GetAsync(userId);
            return OkDone(result);
        }
        [HttpPost(Name = "AddUserRoles")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> AddUserRoles([FromBody] UserRoleMappingCreateDeleteDto userRole)
        {
            var result = await _userRoleService.CreateAsync(userRole);
            return OkDone(result);
        }
        [HttpPut(Name = "UpdateUserRoles")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UserRoleMappingDto userRole)
        {
            var result = await _userRoleService.UpdateAsync(userRole);
            return OkDone(result);
        }

        [HttpDelete(Name = "DeleteUserRoles")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> DeleteUserRoles(UserRoleMappingCreateDeleteDto userRole)
        {
            var result = await _userRoleService.DeleteAsync(userRole);
            return OkDone(result);
        }
    }
}
