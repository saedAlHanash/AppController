using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addfiletoproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "FileRecordId",
                table: "Products",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileRecordId",
                table: "Products",
                column: "FileRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileRecords_FileRecordId",
                table: "Products",
                column: "FileRecordId",
                principalTable: "FileRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileRecords_FileRecordId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileRecordId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileRecordId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
