using Microsoft.EntityFrameworkCore.Migrations;

namespace CarTireRepairService.Data.Migrations
{
    public partial class Carservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvidedServices",
                table: "Workshops");

            migrationBuilder.AddColumn<int>(
                name: "ProvidedServicesID",
                table: "Workshops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_ProvidedServicesID",
                table: "Workshops",
                column: "ProvidedServicesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_CarServices_ProvidedServicesID",
                table: "Workshops",
                column: "ProvidedServicesID",
                principalTable: "CarServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_CarServices_ProvidedServicesID",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_ProvidedServicesID",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "ProvidedServicesID",
                table: "Workshops");

            migrationBuilder.AddColumn<string>(
                name: "ProvidedServices",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
