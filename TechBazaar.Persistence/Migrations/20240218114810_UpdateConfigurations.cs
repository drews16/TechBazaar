using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBazaar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "OrderProducts",
                newName: "OrderProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderProductId",
                table: "OrderProducts",
                newName: "Order");
        }
    }
}
