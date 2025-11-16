using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class AddCartAndCartItems : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Carts",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Carts", x => x.Id);
					table.ForeignKey(
						name: "FK_Carts_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "CartItems",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Quantity = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ProductId = table.Column<int>(type: "int", nullable: false),
					CartId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CartItems", x => x.Id);
					table.ForeignKey(
						name: "FK_CartItems_Carts_CartId",
						column: x => x.CartId,
						principalTable: "Carts",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_CartItems_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_CartItems_CartId",
				table: "CartItems",
				column: "CartId");

			migrationBuilder.CreateIndex(
				name: "IX_CartItems_ProductId",
				table: "CartItems",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Carts_UserId",
				table: "Carts",
				column: "UserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CartItems");

			migrationBuilder.DropTable(
				name: "Carts");
		}
	}
}
