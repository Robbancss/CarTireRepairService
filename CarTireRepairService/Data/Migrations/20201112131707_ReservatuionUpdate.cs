using Microsoft.EntityFrameworkCore.Migrations;

namespace CarTireRepairService.Data.Migrations
{
    public partial class ReservatuionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Workshops_WorkshopID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopID",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Workshops_WorkshopID",
                table: "Reservations",
                column: "WorkshopID",
                principalTable: "Workshops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Workshops_WorkshopID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopID",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Workshops_WorkshopID",
                table: "Reservations",
                column: "WorkshopID",
                principalTable: "Workshops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
