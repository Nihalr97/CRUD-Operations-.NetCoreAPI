using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audree.Demo.Infrastructure.Migrations
{
    public partial class PLClubMigra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Admin");

            migrationBuilder.CreateTable(
                name: "PLClub",
                schema: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plname = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    seasonrank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLClub", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLClub",
                schema: "Admin");
        }
    }
}
