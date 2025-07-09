using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Pictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_location_picture_picture_id",
                table: "location");

            migrationBuilder.DropIndex(
                name: "ix_location_picture_id",
                table: "location");

            migrationBuilder.DropColumn(
                name: "picture_id",
                table: "location");

            migrationBuilder.CreateTable(
                name: "location_picture_bind",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    picture_id = table.Column<string>(type: "text", nullable: false),
                    is_avatar = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_picture_bind", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_picture_bind_location_location_id",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_location_picture_bind_picture_picture_id",
                        column: x => x.picture_id,
                        principalTable: "picture",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_location_picture_bind_location_id",
                table: "location_picture_bind",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_picture_bind_picture_id_location_id",
                table: "location_picture_bind",
                columns: new[] { "picture_id", "location_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "location_picture_bind");

            migrationBuilder.AddColumn<string>(
                name: "picture_id",
                table: "location",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_location_picture_id",
                table: "location",
                column: "picture_id");

            migrationBuilder.AddForeignKey(
                name: "fk_location_picture_picture_id",
                table: "location",
                column: "picture_id",
                principalTable: "picture",
                principalColumn: "id");
        }
    }
}
