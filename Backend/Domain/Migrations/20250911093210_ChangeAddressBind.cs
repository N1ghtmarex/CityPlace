using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddressBind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "location");

            migrationBuilder.AddColumn<string>(
                name: "address_id",
                table: "location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_location_address_id",
                table: "location",
                column: "address_id");

            migrationBuilder.AddForeignKey(
                name: "fk_location_address_address_id",
                table: "location",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_location_address_address_id",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_address_id",
                table: "location");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "location");

            migrationBuilder.AddColumn<Address>(
                name: "address",
                table: "location",
                type: "jsonb",
                nullable: false);
        }
    }
}
