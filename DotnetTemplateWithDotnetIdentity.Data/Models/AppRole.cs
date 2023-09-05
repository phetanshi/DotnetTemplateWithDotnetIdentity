using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblAppRoles")]
    public class AppRole
    {
        public AppRole()
        {
            AppUserRoleMappings = new HashSet<AppUserRoleMapping>();
            AppRoleMenuMappings = new HashSet<AppRoleMenuMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppRoleId { get; set; }
        public string Role { get; set; }
        public string? RoleDesc { get; set; }
        public bool IsActive { get; set; }

        public ICollection<AppUserRoleMapping>? AppUserRoleMappings { get; set; }
        public ICollection<AppRoleMenuMapping>? AppRoleMenuMappings { get; set; }
    }
}
