using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserTestsDataLayer.Migrations
{
    public partial class GoalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Goals");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlacedDate",
                table: "Goals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacedDate",
                table: "Goals");

            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Goals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
