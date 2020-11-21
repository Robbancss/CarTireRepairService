using Microsoft.EntityFrameworkCore.Migrations;

namespace CarTireRepairService.Data.Migrations
{
    public partial class ReservationUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedServices_Reservations_ReservationID",
                table: "ProvidedServices");

            migrationBuilder.DropIndex(
                name: "IX_ProvidedServices_ReservationID",
                table: "ProvidedServices");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "ProvidedServices");

            migrationBuilder.AddColumn<int>(
                name: "ProvidedServiceID",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ProvidedServiceID",
                table: "Reservations",
                column: "ProvidedServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ProvidedServices_ProvidedServiceID",
                table: "Reservations",
                column: "ProvidedServiceID",
                principalTable: "ProvidedServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ProvidedServices_ProvidedServiceID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ProvidedServiceID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ProvidedServiceID",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationID",
                table: "ProvidedServices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedServices_ReservationID",
                table: "ProvidedServices",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedServices_Reservations_ReservationID",
                table: "ProvidedServices",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
