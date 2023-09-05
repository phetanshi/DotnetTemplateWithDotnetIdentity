using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblAppRoleMenuMappings")]
    public class AppRoleMenuMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppRoleMenuMappingId { get; set; }

        public int? AppMenuGroupId { get; set; }
        [ForeignKey(nameof(AppMenuGroupId))]
        public AppMenuGroup? MenuGroup { get; set; }

        public int? AppRoleId { get; set; }
        [ForeignKey(nameof(AppRoleId))]
        public AppRole? Role { get; set; }

        public bool IsActive { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
