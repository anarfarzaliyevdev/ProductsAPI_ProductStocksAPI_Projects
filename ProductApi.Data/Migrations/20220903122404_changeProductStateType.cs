using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductApi.Data.Migrations
{
    public partial class changeProductStateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "State",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
