using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseMigration.Migrations
{
    public partial class dgloing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1Id",
                table: "Lodging1s");

            migrationBuilder.RenameColumn(
                name: "Destination1Id",
                table: "Lodging1s",
                newName: "Destination1122Id");

            migrationBuilder.RenameIndex(
                name: "IX_Lodging1s_Destination1Id",
                table: "Lodging1s",
                newName: "IX_Lodging1s_Destination1122Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1122Id",
                table: "Lodging1s",
                column: "Destination1122Id",
                principalTable: "Destination1s",
                principalColumn: "Destination1Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1122Id",
                table: "Lodging1s");

            migrationBuilder.RenameColumn(
                name: "Destination1122Id",
                table: "Lodging1s",
                newName: "Destination1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Lodging1s_Destination1122Id",
                table: "Lodging1s",
                newName: "IX_Lodging1s_Destination1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodging1s_Destination1s_Destination1Id",
                table: "Lodging1s",
                column: "Destination1Id",
                principalTable: "Destination1s",
                principalColumn: "Destination1Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
