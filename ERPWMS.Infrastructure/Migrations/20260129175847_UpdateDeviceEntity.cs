using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "scada",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveTime",
                schema: "scada",
                table: "Devices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "scada",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LastActiveTime",
                schema: "scada",
                table: "Devices");
        }
    }
}
