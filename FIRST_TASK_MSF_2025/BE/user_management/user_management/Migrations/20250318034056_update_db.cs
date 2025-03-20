using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_management.Migrations
{
    /// <inheritdoc />
    public partial class update_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBody",
                table: "apiLog");

            migrationBuilder.DropColumn(
                name: "ResponseBody",
                table: "apiLog");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "apiLog",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "apiLog",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "TimeLimit",
                table: "apiLog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "apiLog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "apiLog");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "apiLog");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "apiLog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "apiLog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                table: "apiLog",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponseBody",
                table: "apiLog",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "");
        }
    }
}
