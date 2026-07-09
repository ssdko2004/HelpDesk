using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Code { get; set; }
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        public int DepartmentId { get; set; }
        
        public string? TeamLeadUserId { get; set; }

        public bool IsActive { get; set; } = true;



    }
}