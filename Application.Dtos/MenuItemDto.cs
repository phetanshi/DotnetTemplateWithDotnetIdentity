using Application.Dtos.Enum;

namespace Application.Dtos
{
    public class MenuItemDto
    {
        public int AppMenuItemId { get; set; }
        public string MenuItem { get; set; }
        public string? MenuItemDesc { get; set; }
        public bool IsActive { get; set; }

        public int? AppMenuGroupId { get; set; }
    }
}
