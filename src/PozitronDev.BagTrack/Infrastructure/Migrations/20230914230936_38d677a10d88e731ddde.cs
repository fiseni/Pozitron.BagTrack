using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _38d677a10d88e731ddde : Migration
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
                    IsResponseNeeded = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    JulianDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditCreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuditCreatedByUsername = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuditModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditModifiedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
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
