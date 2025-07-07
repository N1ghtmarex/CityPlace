using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
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
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<Address>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_favorite",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_favorite", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_favorite_location_location_id",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_favorite_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_favorite_location_id",
                table: "user_favorite",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_favorite_user_id",
                table: "user_favorite",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "user_favorite");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
