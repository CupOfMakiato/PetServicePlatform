using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Service");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Service");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
