using Application.Dtos;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;

namespace DotnetTemplateWithDotnetIdentity.Api.Services.Definitions
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IRepository repository, ILogger<UserService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }

        public async Task<List<UserReadDto>> GetAsync()
        {
            var result = await Repository.GetList<User>().ToListAsync();
            return Mapper.Map<List<UserReadDto>>(result);
        }
        public async Task<UserReadDto> GetAsync(int employeeId)
        {
            var result = await Repository.GetSingleAsync<User>(employeeId);
            return Mapper.Map<UserReadDto>(result);
        }

        public async Task<UserReadDto> GetAsync(string loginUserId)
        {
            var result = await Repository.GetSingleAsync<User>(x => x.UserName.ToLower() == loginUserId.ToLower());
            return Mapper.Map<UserReadDto>(result);
        }

        public async Task<UserReadDto> CreateAsync(UserCreateDto empDto)
        {
            User emp = new User();
            Mapper.Map(empDto, emp);
            emp.SetDefaultsForAuditFields(GetLoginUserId());
            await Repository.CreateAsync(emp);
            return Mapper.Map<UserReadDto>(emp);
        }

        public async Task<UserReadDto> UpdateAsync(UserUpdateDto empDto)
        {
            User emp = await Repository.GetSingleAsync<User>(empDto.UserId);
            Mapper.Map(empDto, emp);
            emp.SetDefaultsForAuditFields(GetLoginUserId());
            await Repository.UpdateAsync(emp);
            return Mapper.Map<UserReadDto>(emp);
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            await Repository.DeleteAsync<User>(employeeId);
            return true;
        }
    }
}
