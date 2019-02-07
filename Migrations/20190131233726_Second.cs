using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeReddit.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
