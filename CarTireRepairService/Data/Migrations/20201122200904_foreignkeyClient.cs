using Microsoft.EntityFrameworkCore.Migrations;

namespace CarTireRepairService.Data.Migrations
{
    public partial class foreignkeyClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserID",
                table: "Reservations",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
