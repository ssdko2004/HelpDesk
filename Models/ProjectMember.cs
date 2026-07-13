
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models
{
    public class ProjectMember : BaseEntity
    {
        [Required]
        public required Guid ProjectId { get; set; }
        public Project? Project { get; set; }

        [Required]
        public required Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(50)]
        public required string MembershipRole { get; set; }

        public Guid? AddedByUserId { get; set; }
        public ApplicationUser? AddedByUser { get; set; }

        
    }
}