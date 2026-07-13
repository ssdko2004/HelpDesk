
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models
{
    public class TicketComment : BaseEntity
    {
        [Required]
        public required Guid TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        [Required]
        public required Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string CommentText { get; set; }

        public bool IsInternal { get; set; } = false;
    }
}