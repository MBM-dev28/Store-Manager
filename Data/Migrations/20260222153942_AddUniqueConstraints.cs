using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Chains",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Number",
                table: "Stores",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chains_Name",
                table: "Chains",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_Number",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Chains_Name",
                table: "Chains");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Chains",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
