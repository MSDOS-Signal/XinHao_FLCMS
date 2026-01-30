using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "erp",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "erp",
                table: "Products");
        }
    }
}
