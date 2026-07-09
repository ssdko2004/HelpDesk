using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Department : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}