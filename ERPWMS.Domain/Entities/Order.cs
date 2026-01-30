using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNo { get; set; } = string.Empty;
        public string Type { get; set; } = "Purchase"; // Purchase, Sales
        public string CustomerOrSupplier { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Processing, Completed
        public DateTime OrderDate { get; set; }
    }
}
