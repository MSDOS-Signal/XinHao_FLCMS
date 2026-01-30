using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities
{
    public class WorkOrder : BaseEntity
    {
        public string WorkOrderNo { get; set; } = string.Empty;
        public string PlanNo { get; set; } = string.Empty; // Associated APS Plan No
        public string ProductName { get; set; } = string.Empty;
        public int PlanQty { get; set; }
        public int CompletedQty { get; set; }
        public string LineCode { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, Running, Completed, Suspended
        public decimal YieldRate { get; set; } // 良品率 0-100
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
