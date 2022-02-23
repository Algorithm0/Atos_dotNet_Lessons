using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homework3.Migrations
{
    public partial class BookingDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FromUtc",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToUtc",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FromUtc",
                table: "Bookings",
                column: "FromUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ToUtc",
                table: "Bookings",
                column: "ToUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_FromUtc",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ToUtc",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FromUtc",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ToUtc",
                table: "Bookings");
        }
    }
}
