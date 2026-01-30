using ERPWMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            // 1. Seed Products
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "高精密轴承 X-200", Code = "P-1001", Price = 120.50m, Description = "航空级不锈钢轴承", CreatedBy = "System", CreatedTime = DateTime.Now.AddDays(-10) },
                    new Product { Name = "伺服电机 M-500", Code = "P-1002", Price = 2500.00m, Description = "高性能伺服电机，适用于机械臂", CreatedBy = "System", CreatedTime = DateTime.Now.AddDays(-8) },
                    new Product { Name = "工业触控屏 T-10", Code = "P-1003", Price = 850.00m, Description = "10英寸防爆触摸屏", CreatedBy = "System", CreatedTime = DateTime.Now.AddDays(-5) },
                    new Product { Name = "304不锈钢板 2mm", Code = "M-10023", Price = 45.00m, Description = "标准原料板材", CreatedBy = "System", CreatedTime = DateTime.Now.AddDays(-20) },
                    new Product { Name = "控制芯片 C-99", Code = "E-9901", Price = 12.00m, Description = "核心逻辑控制单元", CreatedBy = "System", CreatedTime = DateTime.Now.AddDays(-15) }
                );
                context.SaveChanges();
            }

            // 2. Seed Inventory
            if (!context.Inventories.Any())
            {
                context.Inventories.AddRange(
                    new Inventory { ProductCode = "M-10023", ProductName = "304不锈钢板 2mm", WarehouseCode = "WH-RAW", LocationCode = "A-01-01", BatchNo = "BATCH-20231201", Quantity = 500, CreatedBy = "WMS_System", CreatedTime = DateTime.Now },
                    new Inventory { ProductCode = "P-1001", ProductName = "高精密轴承 X-200", WarehouseCode = "WH-FIN", LocationCode = "B-02-05", BatchNo = "BATCH-20240110", Quantity = 1200, CreatedBy = "WMS_System", CreatedTime = DateTime.Now },
                    new Inventory { ProductCode = "P-1002", ProductName = "伺服电机 M-500", WarehouseCode = "WH-FIN", LocationCode = "B-05-12", BatchNo = "BATCH-20240115", Quantity = 50, CreatedBy = "WMS_System", CreatedTime = DateTime.Now }
                );
                context.SaveChanges();
            }

            // 3. Seed WorkOrders
            if (!context.WorkOrders.Any())
            {
                context.WorkOrders.AddRange(
                    new WorkOrder { WorkOrderNo = "WO-20240129-A01", PlanNo = "PLN-202402", ProductName = "高精密轴承 X-200", PlanQty = 5000, CompletedQty = 3200, LineCode = "Line-A", Status = "Running", YieldRate = 99.8m, StartTime = DateTime.Now.AddHours(-48), CreatedBy = "MES_System", CreatedTime = DateTime.Now.AddDays(-2) },
                    new WorkOrder { WorkOrderNo = "WO-20240129-B05", ProductName = "伺服电机 M-500", PlanQty = 200, CompletedQty = 90, LineCode = "Line-C", Status = "Running", YieldRate = 95.0m, StartTime = DateTime.Now.AddHours(-12), CreatedBy = "MES_System", CreatedTime = DateTime.Now.AddDays(-1) },
                    new WorkOrder { WorkOrderNo = "WO-20240129-C02", ProductName = "工业触控屏 T-10", PlanQty = 1000, CompletedQty = 120, LineCode = "Line-B", Status = "Pending", YieldRate = 0m, StartTime = DateTime.Now.AddHours(2), CreatedBy = "MES_System", CreatedTime = DateTime.Now }
                );
                context.SaveChanges();
            }

            // 4. Seed Orders (Purchase/Sales)
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order { OrderNo = "PO-20240129-001", Type = "Purchase", CustomerOrSupplier = "上海精密机械有限公司", TotalAmount = 58000.00m, Status = "Completed", OrderDate = DateTime.Now.AddDays(-5), CreatedBy = "ERP_System", CreatedTime = DateTime.Now.AddDays(-5) },
                    new Order { OrderNo = "SO-20240129-008", Type = "Sales", CustomerOrSupplier = "特斯拉(上海)超级工厂", TotalAmount = 280000.00m, Status = "Processing", OrderDate = DateTime.Now.AddHours(-4), CreatedBy = "ERP_System", CreatedTime = DateTime.Now.AddHours(-4) }
                );
                context.SaveChanges();
            }

            // 5. Seed Devices
            if (!context.Devices.Any())
            {
                context.Devices.AddRange(
                    new Device { DeviceCode = "CNC-001", DeviceName = "数控加工中心", Status = "Running", RealTimeDataJson = "{\"Speed\": 1200, \"Temp\": 45, \"Current\": 8.5}", CreatedBy = "SCADA_System", CreatedTime = DateTime.Now },
                    new Device { DeviceCode = "ROBOT-ARM-02", DeviceName = "焊接机器人", Status = "Alarm", LastAlarmMessage = "E-503 (伺服过载)", CreatedBy = "SCADA_System", CreatedTime = DateTime.Now },
                    new Device { DeviceCode = "AGV-05", DeviceName = "物流小车", Status = "Charging", RealTimeDataJson = "{\"Battery\": 85, \"Location\": \"Zone-A\"}", CreatedBy = "SCADA_System", CreatedTime = DateTime.Now }
                );
                context.SaveChanges();
            }

            // 6. Seed SRM Suppliers
            if (!context.Suppliers.Any())
            {
                context.Suppliers.AddRange(
                    new Supplier { SupplierName = "宝钢集团", ContactPerson = "张经理", Phone = "13800138000", Email = "zhang@baosteel.com", Address = "上海市宝山区", Rating = "A", Category = "RawMaterial", Status = "Active", CreatedBy = "System" },
                    new Supplier { SupplierName = "西门子自动化", ContactPerson = "李工", Phone = "13900139000", Email = "support@siemens.com", Address = "北京市朝阳区", Rating = "A", Category = "Equipment", Status = "Active", CreatedBy = "System" },
                    new Supplier { SupplierName = "本地五金加工厂", ContactPerson = "王老板", Phone = "13700137000", Email = "wang@localmetal.com", Address = "苏州市工业园区", Rating = "B", Category = "Service", Status = "Active", CreatedBy = "System" }
                );
                context.SaveChanges();
            }

            // 7. Seed TMS Shipments
            if (!context.Shipments.Any())
            {
                context.Shipments.AddRange(
                    new Shipment { ShipmentNo = "SH-20240130-01", OrderNo = "SO-20240129-008", Carrier = "顺丰速运", DriverName = "刘师傅", DriverPhone = "13500001111", VehicleNo = "沪A-88888", Origin = "上海仓库", Destination = "特斯拉工厂", Status = "Shipped", TrackingNumber = "SF100200300", CreatedBy = "TMS_System" },
                    new Shipment { ShipmentNo = "SH-20240130-02", OrderNo = "SO-20240129-009", Carrier = "跨越速运", DriverName = "陈师傅", DriverPhone = "13600002222", VehicleNo = "苏E-66666", Origin = "苏州仓库", Destination = "北京客户", Status = "Pending", TrackingNumber = "", CreatedBy = "TMS_System" }
                );
                context.SaveChanges();
            }

            // 8. Seed QMS QualityChecks
            if (!context.QualityChecks.Any())
            {
                context.QualityChecks.AddRange(
                    new QualityCheck { CheckNo = "QC-20240130-001", TargetType = "Product", TargetCode = "P-1001", CheckType = "Full", SampleQuantity = 100, DefectQuantity = 0, Result = "Pass", Inspector = "质检员A", CreatedBy = "QMS_System" },
                    new QualityCheck { CheckNo = "QC-20240130-002", TargetType = "Material", TargetCode = "M-10023", CheckType = "Sampling", SampleQuantity = 50, DefectQuantity = 2, DefectReason = "表面划痕", Result = "Fail", Inspector = "质检员B", CreatedBy = "QMS_System" }
                );
                context.SaveChanges();
            }

            // 9. Seed EAM Assets
            if (!context.Assets.Any())
            {
                context.Assets.AddRange(
                    new Asset { AssetCode = "AST-001", AssetName = "三菱五轴机床", Model = "M500", SerialNumber = "SN-2022001", Manufacturer = "Mitsubishi", Location = "车间一区", Department = "加工部", PurchaseDate = DateTime.Now.AddYears(-2), PurchasePrice = 1500000m, CurrentValue = 1200000m, Status = "Active", CreatedBy = "EAM_System" },
                    new Asset { AssetCode = "AST-002", AssetName = "海克斯康三坐标", Model = "Global S", SerialNumber = "SN-2023005", Manufacturer = "Hexagon", Location = "质检室", Department = "质量部", PurchaseDate = DateTime.Now.AddYears(-1), PurchasePrice = 900000m, CurrentValue = 800000m, Status = "Maintenance", CreatedBy = "EAM_System" }
                );
                context.SaveChanges();
            }

            // 10. Seed APS ProductionPlans
            if (!context.ProductionPlans.Any())
            {
                context.ProductionPlans.AddRange(
                    new ProductionPlan { PlanNo = "PLN-202402", WorkOrderNo = "WO-20240129-A01", ProductCode = "P-1001", Quantity = 10000, CompletedQuantity = 0, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10), Priority = "High", Status = "Confirmed", CreatedBy = "APS_System" }
                );
                context.SaveChanges();
            }
            
            // Fix existing data if needed (for demo continuity)
            var apsPlan = context.ProductionPlans.FirstOrDefault(p => p.PlanNo == "PLN-202402" || p.PlanNo == "PLAN-202402");
            if (apsPlan != null && string.IsNullOrEmpty(apsPlan.WorkOrderNo))
            {
                apsPlan.WorkOrderNo = "WO-20240129-A01";
                // Standardize PlanNo
                if (apsPlan.PlanNo == "PLAN-202402") apsPlan.PlanNo = "PLN-202402"; 
            }
            
            var mesWo = context.WorkOrders.FirstOrDefault(w => w.WorkOrderNo == "WO-20240129-A01");
            if (mesWo != null && string.IsNullOrEmpty(mesWo.PlanNo))
            {
                mesWo.PlanNo = "PLN-202402";
            }
            context.SaveChanges();
        }
    }
}
