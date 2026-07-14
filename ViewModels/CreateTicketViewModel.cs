
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.ViewModels
{
    public class CreateTicketViewModel
    {
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public required string Description { get; set; }

        public required string Priority { get; set; }
        public Guid CategoryId { get; set; }
        
        public Guid? ProjectId { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}