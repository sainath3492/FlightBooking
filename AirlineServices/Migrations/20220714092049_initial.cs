using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineServices.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline_Master",
                columns: table => new
                {
                    AirlineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From_Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To_Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Scheduled_Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instrument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Business_Seats = table.Column<int>(type: "int", nullable: false),
                    Non_Business_Seats = table.Column<int>(type: "int", nullable: false),
                    Ticket_Cost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rows = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TripType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline_Master", x => x.AirlineID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airline_Master");
        }
    }
}
