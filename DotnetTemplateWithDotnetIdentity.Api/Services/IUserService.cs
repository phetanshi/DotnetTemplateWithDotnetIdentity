using Application.Dtos;

namespace DotnetTemplateWithDotnetIdentity.Api.Services
{
    public interface IUserService
    {
        Task<bool> DeleteEmployeeAsync(int userId);
        Task<List<UserReadDto>> GetAsync();
        Task<UserReadDto> GetAsync(int userId);
        Task<UserReadDto> GetAsync(string loginUserId);
        Task<UserReadDto> CreateAsync(UserCreateDto empDto);
        Task<UserReadDto> UpdateAsync(UserUpdateDto empDto);
    }
}
