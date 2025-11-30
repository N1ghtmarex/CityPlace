using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddressToCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_location_address_address_id",
                table: "location");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropIndex(
                name: "ix_location_address_id",
                table: "location");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "location");

            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "location",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "location",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "ix_location_latitude_longitude",
                table: "location",
                columns: new[] { "latitude", "longitude" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_location_latitude_longitude",
                table: "location");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "location");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "location");

            migrationBuilder.AddColumn<string>(
                name: "address_id",
                table: "location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    appartment = table.Column<string>(type: "text", nullable: false),
                    appartment_fias_id = table.Column<string>(type: "text", nullable: false),
                    district = table.Column<string>(type: "text", nullable: false),
                    district_fias_id = table.Column<string>(type: "text", nullable: false),
                    house = table.Column<string>(type: "text", nullable: false),
                    house_fias_id = table.Column<string>(type: "text", nullable: false),
                    planning_structure = table.Column<string>(type: "text", nullable: false),
                    planning_structure_fias_id = table.Column<string>(type: "text", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    region_fias_id = table.Column<string>(type: "text", nullable: false),
                    settlement = table.Column<string>(type: "text", nullable: false),
                    settlement_fias_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                });

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
    }
}
