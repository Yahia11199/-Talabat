using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class AddPaymentTableAndPaymentFieldsToOrderTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "IsPaid",
				table: "Orders",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<DateTime>(
				name: "PaidAt",
				table: "Orders",
				type: "datetime2",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "Payments",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					OrderId = table.Column<int>(type: "int", nullable: false),
					Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
					PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					IsSuccess = table.Column<bool>(type: "bit", nullable: false),
					PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Payments", x => x.Id);
					table.ForeignKey(
						name: "FK_Payments_Orders_OrderId",
						column: x => x.OrderId,
						principalTable: "Orders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_OrderItems_ProductId",
				table: "OrderItems",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Payments_OrderId",
				table: "Payments",
				column: "OrderId");

			migrationBuilder.CreateIndex(
				name: "IX_Payments_TransactionId",
				table: "Payments",
				column: "TransactionId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_OrderItems_Products_ProductId",
				table: "OrderItems",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_OrderItems_Products_ProductId",
				table: "OrderItems");

			migrationBuilder.DropTable(
				name: "Payments");

			migrationBuilder.DropIndex(
				name: "IX_OrderItems_ProductId",
				table: "OrderItems");

			migrationBuilder.DropColumn(
				name: "IsPaid",
				table: "Orders");

			migrationBuilder.DropColumn(
				name: "PaidAt",
				table: "Orders");
		}
	}
}
