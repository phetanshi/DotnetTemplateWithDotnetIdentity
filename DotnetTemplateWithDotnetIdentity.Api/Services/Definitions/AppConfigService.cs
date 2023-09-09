using Application.Dtos;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;

namespace DotnetTemplateWithDotnetIdentity.Api.Services.Definitions
{
    public class AppConfigService : ServiceBase, IAppConfigService
    {
        public AppConfigService(IRepository repository, ILogger<AppConfigService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }

        public async Task<List<AppConfigDto>> GetAsync()
        {
            var configs = await Repository.GetListAsync<AppConfig>();
            return Mapper.Map<List<AppConfigDto>>(configs);
        }

        public async Task<AppConfigDto> GetAsync(string configKey)
        {
            var config = await Repository.GetSingleAsync<AppConfig>(x => x.ConfigKey.ToLower() == configKey.ToLower());
            return Mapper.Map<AppConfigDto>(config);
        }

        public async Task<List<AppConfigDto>> SearchAsync(SearchAppConfigDto SearchString)
        {
            var config = await Repository.GetListAsync<AppConfig>(x => x.ConfigKey.ToLower() == SearchString.SearchString.ToLower() || x.ConfigValue.ToLower() == SearchString.SearchString.ToLower() || x.ConfigId.ToString() == SearchString.SearchString);
            return Mapper.Map<List<AppConfigDto>>(config.Distinct());
        }
        public async Task<AppConfigDto> CreateAsync(AppConfigCreateDto appConfig)
        {
            var config = Mapper.Map<AppConfig>(appConfig);
            await Repository.CreateAsync(config);
            return Mapper.Map<AppConfigDto>(config);
        }

        public async Task<bool> DeleteAsync(string configKey)
        {
            await Repository.DeleteAsync<AppConfig>(x => x.ConfigKey.ToLower() == configKey.ToLower());
            return true;
        }
        public async Task<AppConfigDto> InactivateAsync(string configKey)
        {
            var config = await Repository.GetSingleAsync<AppConfig>(x => x.ConfigKey.ToLower() == configKey.ToLower());
            config.IsActive = false;
            await Repository.UpdateAsync(config);
            return Mapper.Map<AppConfigDto>(config);
        }

        public async Task<AppConfigDto> UpdateAsync(AppConfigDto appConfig)
        {
            var config = Mapper.Map<AppConfig>(appConfig);
            await Repository.UpdateAsync(config);
            return Mapper.Map<AppConfigDto>(config);
        }
    }
}
