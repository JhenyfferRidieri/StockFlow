using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Materials_MaterialId1",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Materials_MaterialId1",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_MaterialId1",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_MaterialId1",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "MaterialId1",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "MaterialId1",
                table: "Inventory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialId1",
                table: "SaleItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId1",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_MaterialId1",
                table: "SaleItems",
                column: "MaterialId1");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MaterialId1",
                table: "Inventory",
                column: "MaterialId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Materials_MaterialId1",
                table: "Inventory",
                column: "MaterialId1",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Materials_MaterialId1",
                table: "SaleItems",
                column: "MaterialId1",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
