using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities;

// SRM - 供应商管理
public class Supplier : BaseEntity
{
    public string SupplierName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty; // 税号
    public string BankAccount { get; set; } = string.Empty; // 银行账号
    public string PaymentTerms { get; set; } = "Net30"; // 付款条款
    public string Rating { get; set; } = "A"; // A, B, C, D
    public string Status { get; set; } = "Active"; // Active, Inactive, Blacklisted
    public string Category { get; set; } = "RawMaterial"; // RawMaterial, Service, Equipment
}

// TMS - 物流运输
public class Shipment : BaseEntity
{
    public string ShipmentNo { get; set; } = string.Empty;
    public string OrderNo { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public string DriverPhone { get; set; } = string.Empty;
    public string VehicleNo { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime? EstimatedArrival { get; set; }
    public DateTime? ActualArrival { get; set; }
    public decimal ShippingCost { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Shipped, InTransit, Delivered, Delayed
    public string TrackingNumber { get; set; } = string.Empty;
}

// QMS - 质量管理
public class QualityCheck : BaseEntity
{
    public string CheckNo { get; set; } = string.Empty;
    public string TargetType { get; set; } = "Product"; // Product, Material, WorkOrder
    public string TargetCode { get; set; } = string.Empty; // 产品编号/物料编号
    public string BatchNo { get; set; } = string.Empty;
    public string CheckType { get; set; } = "Sampling"; // Full, Sampling, Patrol
    public decimal SampleQuantity { get; set; }
    public decimal DefectQuantity { get; set; }
    public string DefectReason { get; set; } = string.Empty;
    public string Result { get; set; } = "Pass"; // Pass, Fail, Concession
    public string Inspector { get; set; } = string.Empty;
    public DateTime CheckTime { get; set; } = DateTime.Now;
    public string Remarks { get; set; } = string.Empty;
    public Guid? SupplierId { get; set; } // Associated Supplier for SRM stats
}

// APS - 高级排程
public class ProductionPlan : BaseEntity
{
    public string PlanNo { get; set; } = string.Empty;
    public string WorkOrderNo { get; set; } = string.Empty; // Associated MES Work Order No
    public string ProductCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int CompletedQuantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Priority { get; set; } = "Normal"; // High, Normal, Low
    public string WorkCenter { get; set; } = string.Empty;
    public string Status { get; set; } = "Draft"; // Draft, Confirmed, Released, InProgress, Completed
    public string BomVersion { get; set; } = string.Empty;
}

// EAM - 资产管理
public class Asset : BaseEntity
{
    public string AssetCode { get; set; } = string.Empty;
    public string AssetName { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal CurrentValue { get; set; }
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string Status { get; set; } = "Active"; // Active, Maintenance, Scrapped, Idle
    public string MaintenanceLog { get; set; } = "[]"; // JSON Array of logs
}

// MOM - 制造运营 (工序/工艺)
public class Operation : BaseEntity
{
    public string OperationCode { get; set; } = string.Empty;
    public string OperationName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string WorkCenter { get; set; } = string.Empty;
    public decimal SetupTimeMinutes { get; set; }
    public decimal RunTimeSeconds { get; set; }
    public string MachineType { get; set; } = string.Empty;
    public string SkillRequired { get; set; } = string.Empty;
    public bool IsBottleneck { get; set; } = false;
}

// BOM - 物料清单
public class BomItem : BaseEntity
{
    public string ParentProductCode { get; set; } = string.Empty;
    public string ComponentCode { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = "PCS";
    public int Level { get; set; } = 1;
    public string Version { get; set; } = "v1.0";
    public DateTime EffectivityDate { get; set; } = DateTime.Now;
    public decimal ScrapRate { get; set; } // 损耗率 %
    public bool IsSubstituteAllowed { get; set; } = false;
    public string Remarks { get; set; } = string.Empty;
}
