using Application.Dtos;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Constants;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppBaseController
    {
        private readonly IUserService _employeeService;

        public UserController(IUserService employeeService, IConfiguration config, ILogger<UserController> logger) : base(config, logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet(Name = "GetAllEmployees")]
        [Authorize(Policy = AppPolicies.DEFAULT)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.GetAsync();
            return OkDone(result);
        }

        [HttpGet("{employeeId:int}", Name = "GetEmployee")]
        [Authorize(Policy = AppPolicies.DEFAULT)]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var result = await _employeeService.GetAsync(employeeId);
            return OkDone(result);
        }

        [HttpPost(Name = "CreateEmployee")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> CreateEmployee([FromBody] UserCreateDto emp)
        {
            var result = await _employeeService.CreateAsync(emp);
            if (result.UserId > 0)
                return OkDone(result);
            else
                return InternalServerError();
        }

        [HttpPut(Name = "UpdateEmployee")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> UpdateEmployee([FromBody] UserUpdateDto emp)
        {
            var result = await _employeeService.UpdateAsync(emp);
            return OkDone(result);
        }

        [HttpDelete(Name = "DeleteEmployee")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> DeleteEmployee([FromBody] int empId)
        {
            var isSuccess = await _employeeService.DeleteEmployeeAsync(empId);

            if (isSuccess)
                return OkDone(isSuccess);
            else
                return InternalServerError();
        }
    }
}
