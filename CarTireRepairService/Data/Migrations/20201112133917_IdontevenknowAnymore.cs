using Microsoft.EntityFrameworkCore.Migrations;

namespace CarTireRepairService.Data.Migrations
{
    public partial class IdontevenknowAnymore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarServices_Reservations_ReservationID",
                table: "CarServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_CarServices_ProvidedServicesID",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarServices",
                table: "CarServices");

            migrationBuilder.RenameTable(
                name: "CarServices",
                newName: "ProvidedServices");

            migrationBuilder.RenameIndex(
                name: "IX_CarServices_ReservationID",
                table: "ProvidedServices",
                newName: "IX_ProvidedServices_ReservationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvidedServices",
                table: "ProvidedServices",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedServices_Reservations_ReservationID",
                table: "ProvidedServices",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_ProvidedServices_ProvidedServicesID",
                table: "Workshops",
                column: "ProvidedServicesID",
                principalTable: "ProvidedServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedServices_Reservations_ReservationID",
                table: "ProvidedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_ProvidedServices_ProvidedServicesID",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvidedServices",
                table: "ProvidedServices");

            migrationBuilder.RenameTable(
                name: "ProvidedServices",
                newName: "CarServices");

            migrationBuilder.RenameIndex(
                name: "IX_ProvidedServices_ReservationID",
                table: "CarServices",
                newName: "IX_CarServices_ReservationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarServices",
                table: "CarServices",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarServices_Reservations_ReservationID",
                table: "CarServices",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_CarServices_ProvidedServicesID",
                table: "Workshops",
                column: "ProvidedServicesID",
                principalTable: "CarServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
