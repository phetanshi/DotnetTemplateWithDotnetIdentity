using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AppConfigDto
    {
        [Required]
        public int ConfigId { get; set; }
        [Required]
        public string ConfigKey { get; set; } = null!;
        [Required]
        public string ConfigValue { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }
    }
}
