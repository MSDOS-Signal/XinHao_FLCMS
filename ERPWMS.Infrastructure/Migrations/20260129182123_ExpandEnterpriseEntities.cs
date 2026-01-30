using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandEnterpriseEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "eam");

            migrationBuilder.EnsureSchema(
                name: "bom");

            migrationBuilder.EnsureSchema(
                name: "mom");

            migrationBuilder.EnsureSchema(
                name: "aps");

            migrationBuilder.EnsureSchema(
                name: "qms");

            migrationBuilder.EnsureSchema(
                name: "tms");

            migrationBuilder.EnsureSchema(
                name: "srm");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Suppliers",
                newSchema: "srm");

            migrationBuilder.RenameTable(
                name: "Shipments",
                newName: "Shipments",
                newSchema: "tms");

            migrationBuilder.RenameTable(
                name: "QualityChecks",
                newName: "QualityChecks",
                newSchema: "qms");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "base",
                newName: "Products",
                newSchema: "erp");

            migrationBuilder.RenameTable(
                name: "ProductionPlans",
                newName: "ProductionPlans",
                newSchema: "aps");

            migrationBuilder.RenameTable(
                name: "Operations",
                newName: "Operations",
                newSchema: "mom");

            migrationBuilder.RenameTable(
                name: "BomItems",
                newName: "BomItems",
                newSchema: "bom");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Assets",
                newSchema: "eam");

            migrationBuilder.RenameColumn(
                name: "StandardTimeSeconds",
                schema: "mom",
                table: "Operations",
                newName: "SetupTimeMinutes");

            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "eam",
                table: "Assets",
                newName: "PurchasePrice");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                schema: "srm",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualArrival",
                schema: "tms",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                schema: "tms",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                schema: "tms",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverPhone",
                schema: "tms",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedArrival",
                schema: "tms",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                schema: "tms",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                schema: "tms",
                table: "Shipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VehicleNo",
                schema: "tms",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                schema: "qms",
                table: "QualityChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDate",
                schema: "qms",
                table: "QualityChecks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CheckType",
                schema: "qms",
                table: "QualityChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DefectQuantity",
                schema: "qms",
                table: "QualityChecks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DefectReason",
                schema: "qms",
                table: "QualityChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "qms",
                table: "QualityChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SampleQuantity",
                schema: "qms",
                table: "QualityChecks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BomVersion",
                schema: "aps",
                table: "ProductionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CompletedQuantity",
                schema: "aps",
                table: "ProductionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                schema: "aps",
                table: "ProductionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkCenter",
                schema: "aps",
                table: "ProductionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "mom",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBottleneck",
                schema: "mom",
                table: "Operations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MachineType",
                schema: "mom",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RunTimeSeconds",
                schema: "mom",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SkillRequired",
                schema: "mom",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectivityDate",
                schema: "bom",
                table: "BomItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSubstituteAllowed",
                schema: "bom",
                table: "BomItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "bom",
                table: "BomItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ScrapRate",
                schema: "bom",
                table: "BomItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "bom",
                table: "BomItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentValue",
                schema: "eam",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "eam",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                schema: "eam",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                schema: "eam",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                schema: "eam",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceDate",
                schema: "eam",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                schema: "eam",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BankAccount",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TaxId",
                schema: "srm",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ActualArrival",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Destination",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DriverName",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DriverPhone",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "EstimatedArrival",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Origin",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "VehicleNo",
                schema: "tms",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "CheckDate",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "CheckType",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "DefectQuantity",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "DefectReason",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "SampleQuantity",
                schema: "qms",
                table: "QualityChecks");

            migrationBuilder.DropColumn(
                name: "BomVersion",
                schema: "aps",
                table: "ProductionPlans");

            migrationBuilder.DropColumn(
                name: "CompletedQuantity",
                schema: "aps",
                table: "ProductionPlans");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "aps",
                table: "ProductionPlans");

            migrationBuilder.DropColumn(
                name: "WorkCenter",
                schema: "aps",
                table: "ProductionPlans");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "mom",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "IsBottleneck",
                schema: "mom",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "MachineType",
                schema: "mom",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "RunTimeSeconds",
                schema: "mom",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SkillRequired",
                schema: "mom",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "EffectivityDate",
                schema: "bom",
                table: "BomItems");

            migrationBuilder.DropColumn(
                name: "IsSubstituteAllowed",
                schema: "bom",
                table: "BomItems");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "bom",
                table: "BomItems");

            migrationBuilder.DropColumn(
                name: "ScrapRate",
                schema: "bom",
                table: "BomItems");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "bom",
                table: "BomItems");

            migrationBuilder.DropColumn(
                name: "CurrentValue",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Department",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Model",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceDate",
                schema: "eam",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                schema: "eam",
                table: "Assets");

            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                schema: "srm",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Shipments",
                schema: "tms",
                newName: "Shipments");

            migrationBuilder.RenameTable(
                name: "QualityChecks",
                schema: "qms",
                newName: "QualityChecks");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "erp",
                newName: "Products",
                newSchema: "base");

            migrationBuilder.RenameTable(
                name: "ProductionPlans",
                schema: "aps",
                newName: "ProductionPlans");

            migrationBuilder.RenameTable(
                name: "Operations",
                schema: "mom",
                newName: "Operations");

            migrationBuilder.RenameTable(
                name: "BomItems",
                schema: "bom",
                newName: "BomItems");

            migrationBuilder.RenameTable(
                name: "Assets",
                schema: "eam",
                newName: "Assets");

            migrationBuilder.RenameColumn(
                name: "SetupTimeMinutes",
                table: "Operations",
                newName: "StandardTimeSeconds");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "Assets",
                newName: "Value");
        }
    }
}
