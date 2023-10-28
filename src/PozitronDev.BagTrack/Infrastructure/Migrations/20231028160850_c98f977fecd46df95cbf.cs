using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class c98f977fecd46df95cbf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_ActiveCarousel_Start_Stop",
                table: "Flight",
                columns: new[] { "AirlineIATA", "ActiveCarousel", "Start", "Stop" });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate",
                table: "Flight",
                columns: new[] { "AirlineIATA", "Number", "OriginDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_ActiveCarousel_Start_Stop",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_AirlineIATA_Number_OriginDate",
                table: "Flight");
        }
    }
}
