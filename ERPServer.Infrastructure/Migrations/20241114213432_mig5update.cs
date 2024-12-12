using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig5update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "Orders",
                newName: "TaxRate");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "Invoices",
                newName: "SubTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingFee",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingFee",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TaxRate",
                table: "Orders",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Invoices",
                newName: "DiscountAmount");
        }
    }
}
