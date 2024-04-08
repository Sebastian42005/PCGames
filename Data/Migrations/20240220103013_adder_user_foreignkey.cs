using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCGamesFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class adder_user_foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_id",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_id",
                table: "Order");
        }
    }
}
