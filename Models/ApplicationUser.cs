using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FullName { get; set; }

        public int? DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    }
}