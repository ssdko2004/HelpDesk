using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public abstract class BaseEntity
    {        
        public required Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
