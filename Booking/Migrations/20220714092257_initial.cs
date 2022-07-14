using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight_Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onward_JourneyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return_JourneyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Onward_Meal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return_Meal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onward_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trip_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seat_Numbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Seats = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight_Booking", x => x.BookingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight_Booking");
        }
    }
}
