using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("23fe4e81-5015-42a0-8d76-d1f08c6b227a"), "2025-04-19 01:02:03", "HR", "HR" },
                    { new Guid("5e76c76d-4cf6-4784-8796-fd9f5c8cf29d"), "2025-04-19 01:02:01", "Admin", "ADMIN" },
                    { new Guid("b7a80d7e-24d8-4cc3-8981-ee4e1b5b1b3a"), "2025-04-19 01:02:04", "Author", "AUTHOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("23fe4e81-5015-42a0-8d76-d1f08c6b227a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e76c76d-4cf6-4784-8796-fd9f5c8cf29d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7a80d7e-24d8-4cc3-8981-ee4e1b5b1b3a"));
        }
    }
}
