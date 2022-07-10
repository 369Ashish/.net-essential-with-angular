using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "users",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "users");
        }
    }
}
