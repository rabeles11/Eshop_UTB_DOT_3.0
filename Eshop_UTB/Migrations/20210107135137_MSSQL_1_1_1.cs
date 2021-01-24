using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop_UTB.Migrations
{
    public partial class MSSQL_1_1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StavReklamace",
                table: "Order",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StavReklamace",
                table: "Order");
        }
    }
}
