﻿// <auto-generated />
using System;
using EventTicketBookingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventTicketBookingApi.Migrations
{
    [DbContext(typeof(EventTicketBookingDbContext))]
    [Migration("20241103133554_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventTicketBookingApi.Data.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CancellationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QRCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EventId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.TicketCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("integer");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("TicketCategories");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.Booking", b =>
                {
                    b.HasOne("EventTicketBookingApi.Data.TicketCategory", "Category")
                        .WithMany("Bookings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventTicketBookingApi.Data.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.TicketCategory", b =>
                {
                    b.HasOne("EventTicketBookingApi.Data.Event", "Event")
                        .WithMany("TicketCategories")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.Event", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("TicketCategories");
                });

            modelBuilder.Entity("EventTicketBookingApi.Data.TicketCategory", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
