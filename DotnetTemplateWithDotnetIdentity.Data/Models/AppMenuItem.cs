using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblAppMenuItems")]
    public class AppMenuItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppMenuItemId { get; set; }
        public string MenuItem { get; set; }
        public string? MenuItemDesc { get; set; }
        public bool IsActive { get; set; }

        public int? AppMenuGroupId { get; set; }
        [ForeignKey(nameof(AppMenuGroupId))]
        public AppMenuGroup? AppMenuGroup { get; set; }

    }
}
