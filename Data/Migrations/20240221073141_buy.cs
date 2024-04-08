using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCGamesFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class buy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGames_Category_CategoryId",
                table: "CategoryGames");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGames_Game_GameId",
                table: "CategoryGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrders_Game_GameId",
                table: "GameOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrders_Order_OrderId",
                table: "GameOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_GameOrders_GameId",
                table: "GameOrders");

            migrationBuilder.DropIndex(
                name: "IX_GameOrders_OrderId",
                table: "GameOrders");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGames_CategoryId",
                table: "CategoryGames");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGames_GameId",
                table: "CategoryGames");

            migrationBuilder.DropColumn(
                name: "User_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameOrders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "GameOrders");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CategoryGames");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "CategoryGames");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_userId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameOrders_Game_id",
                table: "GameOrders",
                column: "Game_id");

            migrationBuilder.CreateIndex(
                name: "IX_GameOrders_Order_id",
                table: "GameOrders",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGames_Category_id",
                table: "CategoryGames",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGames_Game_id",
                table: "CategoryGames",
                column: "Game_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGames_Category_Category_id",
                table: "CategoryGames",
                column: "Category_id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGames_Game_Game_id",
                table: "CategoryGames",
                column: "Game_id",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrders_Game_Game_id",
                table: "GameOrders",
                column: "Game_id",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrders_Order_Order_id",
                table: "GameOrders",
                column: "Order_id",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGames_Category_Category_id",
                table: "CategoryGames");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGames_Game_Game_id",
                table: "CategoryGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrders_Game_Game_id",
                table: "GameOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrders_Order_Order_id",
                table: "GameOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_GameOrders_Game_id",
                table: "GameOrders");

            migrationBuilder.DropIndex(
                name: "IX_GameOrders_Order_id",
                table: "GameOrders");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGames_Category_id",
                table: "CategoryGames");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGames_Game_id",
                table: "CategoryGames");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Order",
                newName: "IX_Order_userId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "User_id",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "GameOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CategoryGames",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "CategoryGames",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameOrders_GameId",
                table: "GameOrders",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameOrders_OrderId",
                table: "GameOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGames_CategoryId",
                table: "CategoryGames",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGames_GameId",
                table: "CategoryGames",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGames_Category_CategoryId",
                table: "CategoryGames",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGames_Game_GameId",
                table: "CategoryGames",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrders_Game_GameId",
                table: "GameOrders",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrders_Order_OrderId",
                table: "GameOrders",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
