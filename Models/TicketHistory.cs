
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models
{
    public class TicketHistory : BaseEntity
    {
        [Required]
        public required Guid TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        [Required]
        public required Guid ChangedByUserId { get; set; }
        public ApplicationUser? ChangedByUser { get; set; }

        [Required]
        [MaxLength(50)]
        public required string ActionType { get; set; }

        [MaxLength(1000)]
        public string? OldComment { get; set; }
        
        [MaxLength(1000)]
        public string? NewComment { get; set; }

        public string? Notes { get; set; }
    }
}