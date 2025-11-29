using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLocationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "location",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "location",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    region_fias_id = table.Column<string>(type: "text", nullable: false),
                    district = table.Column<string>(type: "text", nullable: false),
                    district_fias_id = table.Column<string>(type: "text", nullable: false),
                    settlement = table.Column<string>(type: "text", nullable: false),
                    settlement_fias_id = table.Column<string>(type: "text", nullable: false),
                    planning_structure = table.Column<string>(type: "text", nullable: false),
                    planning_structure_fias_id = table.Column<string>(type: "text", nullable: false),
                    house = table.Column<string>(type: "text", nullable: false),
                    house_fias_id = table.Column<string>(type: "text", nullable: false),
                    appartment = table.Column<string>(type: "text", nullable: false),
                    appartment_fias_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_location_created_at",
                table: "location",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_location_id",
                table: "location",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_location_is_archive",
                table: "location",
                column: "is_archive");

            migrationBuilder.CreateIndex(
                name: "ix_location_type",
                table: "location",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_location_updated_at",
                table: "location",
                column: "updated_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropIndex(
                name: "ix_location_created_at",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_id",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_is_archive",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_type",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_updated_at",
                table: "location");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "location",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "location",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
