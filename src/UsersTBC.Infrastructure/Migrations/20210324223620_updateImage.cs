using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersTBC.Infrastructure.Migrations
{
    public partial class updateImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "UserImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "UserImages");
        }
    }
}
