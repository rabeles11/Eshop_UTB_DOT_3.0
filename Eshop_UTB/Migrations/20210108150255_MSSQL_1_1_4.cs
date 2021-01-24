using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop_UTB.Migrations
{
    public partial class MSSQL_1_1_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DuvodReklamace",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuvodReklamace",
                table: "Order");
        }
    }
}
