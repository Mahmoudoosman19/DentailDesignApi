using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CaseTableAdustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedAt",
                schema: "cases",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "DesignertId",
                schema: "cases",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "Model3DPath",
                schema: "cases",
                table: "Cases",
                newName: "CustomerScanPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerScanPath",
                schema: "cases",
                table: "Cases",
                newName: "Model3DPath");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                schema: "cases",
                table: "Cases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DesignertId",
                schema: "cases",
                table: "Cases",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
