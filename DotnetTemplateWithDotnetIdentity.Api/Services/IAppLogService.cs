using Application.Dtos;

namespace DotnetTemplateWithDotnetIdentity.Api.Services
{
    public interface IAppLogService
    {
        Task<List<ActivityLogDto>> GetActivityLogsAsync(SearchCriteria searchCriteria);
        Task<List<ErrorLogDto>> GetErrorLogsAsync(SearchCriteria searchCriteria);
    }
}
