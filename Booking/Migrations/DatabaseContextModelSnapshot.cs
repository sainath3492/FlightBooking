﻿// <auto-generated />
using System;
using Booking.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Booking.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Booking.Database.Flight_Bookings", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Onward_JourneyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Onward_Meal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Onward_Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Return_JourneyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Return_Meal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Return_Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Seat_Numbers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Total_Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Total_Seats")
                        .HasColumnType("int");

                    b.Property<string>("Trip_Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingID");

                    b.ToTable("Flight_Booking");
                });
#pragma warning restore 612, 618
        }
    }
}
