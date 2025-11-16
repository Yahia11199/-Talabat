using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class AddAuditColumnsToStoreAndProductTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "CreatedById",
				table: "Stores",
				type: "nvarchar(450)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedOn",
				table: "Stores",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<string>(
				name: "UpdatedById",
				table: "Stores",
				type: "nvarchar(450)",
				nullable: true);

			migrationBuilder.AddColumn<DateTime>(
				name: "UpdatedOn",
				table: "Stores",
				type: "datetime2",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "CreatedById",
				table: "Products",
				type: "nvarchar(450)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedOn",
				table: "Products",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<string>(
				name: "UpdatedById",
				table: "Products",
				type: "nvarchar(450)",
				nullable: true);

			migrationBuilder.AddColumn<DateTime>(
				name: "UpdatedOn",
				table: "Products",
				type: "datetime2",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Stores_CreatedById",
				table: "Stores",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Stores_UpdatedById",
				table: "Stores",
				column: "UpdatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Products_CreatedById",
				table: "Products",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Products_UpdatedById",
				table: "Products",
				column: "UpdatedById");

			migrationBuilder.AddForeignKey(
				name: "FK_Products_AspNetUsers_CreatedById",
				table: "Products",
				column: "CreatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Products_AspNetUsers_UpdatedById",
				table: "Products",
				column: "UpdatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Stores_AspNetUsers_CreatedById",
				table: "Stores",
				column: "CreatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Stores_AspNetUsers_UpdatedById",
				table: "Stores",
				column: "UpdatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_AspNetUsers_CreatedById",
				table: "Products");

			migrationBuilder.DropForeignKey(
				name: "FK_Products_AspNetUsers_UpdatedById",
				table: "Products");

			migrationBuilder.DropForeignKey(
				name: "FK_Stores_AspNetUsers_CreatedById",
				table: "Stores");

			migrationBuilder.DropForeignKey(
				name: "FK_Stores_AspNetUsers_UpdatedById",
				table: "Stores");

			migrationBuilder.DropIndex(
				name: "IX_Stores_CreatedById",
				table: "Stores");

			migrationBuilder.DropIndex(
				name: "IX_Stores_UpdatedById",
				table: "Stores");

			migrationBuilder.DropIndex(
				name: "IX_Products_CreatedById",
				table: "Products");

			migrationBuilder.DropIndex(
				name: "IX_Products_UpdatedById",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "CreatedById",
				table: "Stores");

			migrationBuilder.DropColumn(
				name: "CreatedOn",
				table: "Stores");

			migrationBuilder.DropColumn(
				name: "UpdatedById",
				table: "Stores");

			migrationBuilder.DropColumn(
				name: "UpdatedOn",
				table: "Stores");

			migrationBuilder.DropColumn(
				name: "CreatedById",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "CreatedOn",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "UpdatedById",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "UpdatedOn",
				table: "Products");
		}
	}
}
