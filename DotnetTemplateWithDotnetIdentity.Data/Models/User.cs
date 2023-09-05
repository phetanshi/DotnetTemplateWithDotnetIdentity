using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTemplateWithDotnetIdentity.Data.Models
{
    [Table("tblUsers")]
    public class User
    {
        public User()
        {
            AppUserRoleMappings = new HashSet<AppUserRoleMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public User? Manager { get; set; }

        public ICollection<AppUserRoleMapping>? AppUserRoleMappings { get; set; }


        public void SetDefaultsForAuditFields(string loginUserId)
        {
            if (UserId > 0)
            {
                UpdatedBy = loginUserId;
                UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                CreatedBy = loginUserId;
                CreatedDate = DateTime.UtcNow;
                UpdatedBy = loginUserId;
                UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}
