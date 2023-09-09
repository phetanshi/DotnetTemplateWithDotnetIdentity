using Application.Dtos;

namespace DotnetTemplateWithDotnetIdentity.Api.Services
{
    public interface IAppConfigService
    {
        Task<List<AppConfigDto>> GetAsync();
        Task<AppConfigDto> GetAsync(string configKey);

        Task<AppConfigDto> CreateAsync(AppConfigCreateDto appConfig);
        Task<AppConfigDto> UpdateAsync(AppConfigDto appConfig);
        Task<AppConfigDto> InactivateAsync(string configKey);
        Task<bool> DeleteAsync(string configKey);
        Task<List<AppConfigDto>> SearchAsync(SearchAppConfigDto SearchString);

    }
}
