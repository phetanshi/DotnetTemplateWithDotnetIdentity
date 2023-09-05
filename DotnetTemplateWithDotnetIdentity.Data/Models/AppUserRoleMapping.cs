using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblAppUserRoleMappings")]
    public class AppUserRoleMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppUserRoleMappingId { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }


        public int AppRoleId { get; set; }
        [ForeignKey(nameof(AppRoleId))]
        public AppRole? Role { get; set; }


        public bool IsActive { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
