using Microsoft.EntityFrameworkCore.Migrations;

namespace Audree.Demo.Infrastructure.Migrations
{
    public partial class addnullalable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "seasonrank",
                schema: "Admin",
                table: "PLClub",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "seasonrank",
                schema: "Admin",
                table: "PLClub",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
