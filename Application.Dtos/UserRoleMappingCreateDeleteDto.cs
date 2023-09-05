using Application.Dtos.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserRoleMappingCreateDeleteDto
    {
        public int UserId { get; set; }
        public AppRoleEnum AppRoleId { get; set; }
    }
}
