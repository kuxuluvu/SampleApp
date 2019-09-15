using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleApp.Migrations
{
    public partial class Add_ImageUrl_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c748e601-e940-49b0-b45c-5ee59e74e188"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                nullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DayOfBirth", "Email", "FirstName", "ImageUrl", "IsActive", "IsDeleted", "LastName", "Password", "Phone", "Salt", "Username" },
                values: new object[] { new Guid("96e70db0-c9e3-4217-bb34-5793cc8e6074"), new DateTime(1999, 9, 15, 21, 42, 30, 199, DateTimeKind.Local), "", "admin", null, true, false, "", "Ijh2z31T4H7SzDlwwfGSpkpsCz9LsRQp2ssiFhjbq1Y=", "", "iF4WdJLv0ZL2Buy45p78Tw==", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("96e70db0-c9e3-4217-bb34-5793cc8e6074"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DayOfBirth", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "Password", "Phone", "Salt", "Username" },
                values: new object[] { new Guid("c748e601-e940-49b0-b45c-5ee59e74e188"), new DateTime(1999, 9, 14, 11, 9, 14, 831, DateTimeKind.Local), "", "admin", true, false, "", "JPDj9knANCG+NekA/09oJ4KJUwu187fYTT4pdtN7n4c=", "", "uDvx/xGDUkhnlAQbh0hVvA==", "admin" });
        }
    }
}
