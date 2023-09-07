using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace case_study_library.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutBook",
                schema: "public",
                table: "table_Book",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvaliable",
                schema: "public",
                table: "table_Book",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutBook",
                schema: "public",
                table: "table_Book");

            migrationBuilder.DropColumn(
                name: "IsAvaliable",
                schema: "public",
                table: "table_Book");
        }
    }
}
