
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models
{
    public class Ticket : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid? CategoryId { get; set; }
        public TicketCategory? Category { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Status { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Priority { get; set; }

        [Required]
        public required Guid RequesterUserId { get; set; }
        public ApplicationUser? RequesterUser { get; set; }

        public Guid? AssignedToUserId { get; set; }
        public ApplicationUser? AssignedToUser { get; set; }

        public Guid? AssignedByUserId { get; set; }
        public ApplicationUser? AssignedByUser { get; set; }

        public DateTime? DueAtUtc { get; set; }

        public DateTime? ClosedAtUtc { get; set; }

        [MaxLength(1000)]
        public string? ResolutionSummary { get; set; }

        [MaxLength(500)]
        public string? RejectionReason { get; set; }
    }
}