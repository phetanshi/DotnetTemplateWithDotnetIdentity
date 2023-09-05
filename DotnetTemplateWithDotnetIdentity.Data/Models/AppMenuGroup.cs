using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblAppMenuGroups")]
    public class AppMenuGroup
    {
        public AppMenuGroup()
        {
            AppMenuItems = new HashSet<AppMenuItem>();
            AppRoleMenuMappings = new HashSet<AppRoleMenuMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppMenuGroupId { get; set; }
        public string AppMenuGroupName { get; set; }
        public bool IsActive { get; set; }

        public ICollection<AppMenuItem>? AppMenuItems { get; set; }
        public ICollection<AppRoleMenuMapping>? AppRoleMenuMappings { get; set; }
    }
}
