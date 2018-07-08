using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseMigration.Migrations
{
    public partial class dgloing3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryContactId",
                table: "Lodgings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondaryContactId",
                table: "Lodgings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_PrimaryContactId",
                table: "Lodgings",
                column: "PrimaryContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_SecondaryContactId",
                table: "Lodgings",
                column: "SecondaryContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Person_PrimaryContactId",
                table: "Lodgings",
                column: "PrimaryContactId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Person_SecondaryContactId",
                table: "Lodgings",
                column: "SecondaryContactId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Person_PrimaryContactId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Person_SecondaryContactId",
                table: "Lodgings");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Lodgings_PrimaryContactId",
                table: "Lodgings");

            migrationBuilder.DropIndex(
                name: "IX_Lodgings_SecondaryContactId",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "PrimaryContactId",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "SecondaryContactId",
                table: "Lodgings");
        }
    }
}
