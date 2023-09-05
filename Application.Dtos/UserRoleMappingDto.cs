using Application.Dtos.Enum;

namespace Application.Dtos
{
    public class UserRoleMappingDto
    {
        public int AppUserRoleMappingId { get; set; }
        public int UserId { get; set; }
        public AppRoleEnum AppRoleId { get; set; }
    }
}
