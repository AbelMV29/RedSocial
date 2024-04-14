using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.mvc.Migrations
{
    /// <inheritdoc />
    public partial class addcolumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "ProfileUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "ProfileUser");
        }
    }
}
