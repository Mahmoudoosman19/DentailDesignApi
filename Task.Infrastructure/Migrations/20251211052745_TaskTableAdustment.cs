using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskTableAdustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                schema: "tasks",
                table: "CaseTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model3DPath",
                schema: "tasks",
                table: "CaseTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "tasks",
                table: "CaseTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedAt",
                schema: "tasks",
                table: "CaseTasks");

            migrationBuilder.DropColumn(
                name: "Model3DPath",
                schema: "tasks",
                table: "CaseTasks");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "tasks",
                table: "CaseTasks");
        }
    }
}
