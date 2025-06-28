using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { -1, "create_user", "allowed", new Guid("83a74dcd-28de-4e38-d4c8-08ddacc2647a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
