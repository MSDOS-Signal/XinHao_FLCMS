using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceLogToAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckDate",
                schema: "qms",
                table: "QualityChecks",
                newName: "CheckTime");

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceLog",
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
                name: "MaintenanceLog",
                schema: "eam",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "CheckTime",
                schema: "qms",
                table: "QualityChecks",
                newName: "CheckDate");
        }
    }
}
