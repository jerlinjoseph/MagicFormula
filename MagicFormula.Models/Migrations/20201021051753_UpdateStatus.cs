using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicFormula.Models.Migrations
{
    public partial class UpdateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GuruFocusUpdateStatus",
                table: "Stocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MsnUpdateStatus",
                table: "Stocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RuleOneUpdateStatus",
                table: "Stocks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuruFocusUpdateStatus",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "MsnUpdateStatus",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "RuleOneUpdateStatus",
                table: "Stocks");
        }
    }
}
