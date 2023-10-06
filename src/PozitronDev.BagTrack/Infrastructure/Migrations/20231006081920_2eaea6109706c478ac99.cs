using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2eaea6109706c478ac99 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight");

            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "Flight",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstBag",
                table: "Flight",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastBag",
                table: "Flight",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight",
                columns: new[] { "AirlineIATA", "Number", "OriginDate", "IsDeleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "Agent",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "FirstBag",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "LastBag",
                table: "Flight");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight",
                columns: new[] { "AirlineIATA", "Number", "OriginDate", "IsDeleted" })
                .Annotation("SqlServer:Include", new[] { "ActiveCarousel", "AllocatedCarousel", "Start", "Stop" });
        }
    }
}
