using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bdb7e8b26614519ec58d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_IsDeleted",
                table: "OutboxMessage");

            migrationBuilder.DropIndex(
                name: "IX_InboxMessage_IsDeleted",
                table: "InboxMessage");

            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_ActiveCarousel_IsDeleted_Start_Stop",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_IsDeleted",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Bag_IsDeleted",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OutboxMessage");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InboxMessage");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OutboxMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InboxMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Flight",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_IsDeleted",
                table: "OutboxMessage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessage_IsDeleted",
                table: "InboxMessage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_ActiveCarousel_IsDeleted_Start_Stop",
                table: "Flight",
                columns: new[] { "AirlineIATA", "ActiveCarousel", "IsDeleted", "Start", "Stop" })
                .Annotation("SqlServer:Include", new[] { "Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted",
                table: "Flight",
                columns: new[] { "AirlineIATA", "Number", "OriginDate", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_IsDeleted",
                table: "Flight",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Bag_IsDeleted",
                table: "Bag",
                column: "IsDeleted");
        }
    }
}
