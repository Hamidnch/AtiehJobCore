using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtiehJobCore.Data.Migrations
{
    public partial class Initial971104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Placements",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Jobseekers",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Employers",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Placements");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Jobseekers");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Employers");
        }
    }
}
