using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobiroller.Migrations
{
    public partial class AddIncidentTable_Column_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Incidents");
        }
    }
}
