using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMesApsLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanNo",
                schema: "mes",
                table: "WorkOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderNo",
                schema: "aps",
                table: "ProductionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanNo",
                schema: "mes",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkOrderNo",
                schema: "aps",
                table: "ProductionPlans");
        }
    }
}
