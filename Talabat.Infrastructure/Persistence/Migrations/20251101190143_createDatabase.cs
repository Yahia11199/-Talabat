using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class createDatabase : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfae565aec1",
				columns: new[] { "EmailConfirmed", "PasswordHash" },
				values: new object[] { true, "AQAAAAIAAYagAAAAEA1i8cglS5qfxLtBNNbq+cS2B+GZXJU6Td1Rh+R8g5fRloZLzppC5/CltpfQZB0g9g==" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019a4071-fc29-767d-b6e9-9bfae565aec1",
				columns: new[] { "EmailConfirmed", "PasswordHash" },
				values: new object[] { false, "AQAAAAIAAYagAAAAEKUd3b3kmB8/jcHfTkTKXGRQHHyhIN1TfxB6Cf3IkcvGCn7gYdgRi0zObx+4plc0Ug==" });
		}
	}
}
