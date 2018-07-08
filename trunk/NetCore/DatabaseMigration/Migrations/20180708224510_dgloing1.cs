using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseMigration.Migrations
{
    public partial class dgloing1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodging1s_Destinations_DestinationId",
                table: "Lodging1s");

            migrationBuilder.DropIndex(
                name: "IX_Lodging1s_DestinationId",
                table: "Lodging1s");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Lodging1s");

            migrationBuilder.CreateIndex(
                name: "IX_Lodging1s_Destination1Id",
                table: "Lodging1s",
                column: "Destination1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1Id",
                table: "Lodging1s",
                column: "Destination1Id",
                principalTable: "Destination1s",
                principalColumn: "Destination1Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1Id",
                table: "Lodging1s");

            migrationBuilder.DropIndex(
                name: "IX_Lodging1s_Destination1Id",
                table: "Lodging1s");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Lodging1s",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lodging1s_DestinationId",
                table: "Lodging1s",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodging1s_Destinations_DestinationId",
                table: "Lodging1s",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
