using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseMigration.Migrations
{
    public partial class addphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Person_PrimaryContactId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Person_SecondaryContactId",
                table: "Lodgings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "People");

            migrationBuilder.RenameColumn(
                name: "SecondaryContactId",
                table: "Lodgings",
                newName: "SecondaryContactPersonId");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactId",
                table: "Lodgings",
                newName: "PrimaryContactPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lodgings_SecondaryContactId",
                table: "Lodgings",
                newName: "IX_Lodgings_SecondaryContactPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lodgings_PrimaryContactId",
                table: "Lodgings",
                newName: "IX_Lodgings_PrimaryContactPersonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "People",
                newName: "PersonId");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "People",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialSecurityNumber",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "PersonId");

            migrationBuilder.CreateTable(
                name: "PersonPhotos",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    Caption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhotos", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonPhotos_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CostUSD = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });

            migrationBuilder.CreateTable(
                name: "Activitys",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activitys", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activitys_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activitys_TripId",
                table: "Activitys",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_People_PrimaryContactPersonId",
                table: "Lodgings",
                column: "PrimaryContactPersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_People_SecondaryContactPersonId",
                table: "Lodgings",
                column: "SecondaryContactPersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_People_PrimaryContactPersonId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_People_SecondaryContactPersonId",
                table: "Lodgings");

            migrationBuilder.DropTable(
                name: "Activitys");

            migrationBuilder.DropTable(
                name: "PersonPhotos");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "People");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNumber",
                table: "People");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Person");

            migrationBuilder.RenameColumn(
                name: "SecondaryContactPersonId",
                table: "Lodgings",
                newName: "SecondaryContactId");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactPersonId",
                table: "Lodgings",
                newName: "PrimaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Lodgings_SecondaryContactPersonId",
                table: "Lodgings",
                newName: "IX_Lodgings_SecondaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Lodgings_PrimaryContactPersonId",
                table: "Lodgings",
                newName: "IX_Lodgings_PrimaryContactId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Person",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

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
    }
}
