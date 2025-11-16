using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Talabat.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class seedIdentityTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "019a4071-fc29-767d-b6e9-9bfca1eadf3b", "019a4071-fc29-767d-b6e9-9bfff0b3536d", false, false, "Admin", "ADMIN" },
					{ "019a4071-fc29-767d-b6e9-9bfd907b282d", "019a4071-fc29-767d-b6e9-9c00c23e2c33", false, false, "Owner", "OWNER" },
					{ "019a4071-fc29-767d-b6e9-9bfe272ea9a1", "019a4071-fc29-767d-b6e9-9c018b89cb34", true, false, "Member", "MEMBER" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "019a4071-fc29-767d-b6e9-9bfae565aec1", 0, "019a4071-fc29-767d-b6e9-9bfbd1302f1e", "admin@talabat.com", false, "Talabat", "Admin", false, null, "ADMIN@TALABAT.COM", "ADMIN@TALABAT.COM", "AQAAAAIAAYagAAAAEKUd3b3kmB8/jcHfTkTKXGRQHHyhIN1TfxB6Cf3IkcvGCn7gYdgRi0zObx+4plc0Ug==", null, false, "94694376AABF4DB291E6B659D313C397", false, "admin@talabat.com" });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "019a4071-fc29-767d-b6e9-9bfca1eadf3b", "019a4071-fc29-767d-b6e9-9bfae565aec1" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfd907b282d");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfe272ea9a1");

			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "019a4071-fc29-767d-b6e9-9bfca1eadf3b", "019a4071-fc29-767d-b6e9-9bfae565aec1" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfca1eadf3b");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfae565aec1");
		}
	}
}
