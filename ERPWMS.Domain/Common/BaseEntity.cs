using System;
using System.ComponentModel.DataAnnotations;

namespace ERPWMS.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "System";
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        
        // Domain Events can be added here
    }

    public interface IAggregateRoot { }
}
