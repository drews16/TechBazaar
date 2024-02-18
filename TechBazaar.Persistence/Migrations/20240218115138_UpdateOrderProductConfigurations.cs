using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBazaar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderProductConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderProducts");
        }
    }
}
