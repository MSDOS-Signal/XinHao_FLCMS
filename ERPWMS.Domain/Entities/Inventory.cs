using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string WarehouseCode { get; set; } = string.Empty; // WH-RAW, WH-FIN
        public string LocationCode { get; set; } = string.Empty; // A-01-01
        public string BatchNo { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "PCS";
        public DateTime? ExpiryDate { get; set; }
    }
}
