using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Migrations
{
    /// <inheritdoc />
    public partial class _41eabab6431c4720c3ad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagTrackId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsResponseNeeded = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    JulianDate = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditCreatedByUserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuditCreatedByUsername = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuditModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditModifiedByUserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuditModifiedByUsername = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bag", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bag_IsDeleted",
                table: "Bag",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bag");
        }
    }
}
