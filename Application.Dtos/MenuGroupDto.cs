using Application.Dtos.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MenuGroupDto
    {
        public MenuGroupDto()
        {
            MenuItems = new List<MenuItemDto>();
        }
        public AppRoleEnum AppRoleId { get; set; }
        public string AppMenuGroupName { get; set; }
        public bool IsActive { get; set; }
        public List<MenuItemDto>? MenuItems { get; set; }

    }
}
