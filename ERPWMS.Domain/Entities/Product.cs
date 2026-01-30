using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Unit { get; set; } = "PCS";
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        
        // Additional properties...
    }
}
