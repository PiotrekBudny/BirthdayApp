﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayTracker.Database.Migrations
{
    [DbContext(typeof(BirthdayDbContext))]
    [Migration("20210302224456_RemoveNotNeededFields")]
    partial class RemoveNotNeededFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("BirthdayApp.Database.Models.BirthdayInfo", b =>
                {
                    b.Property<int>("BirthdayId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayOfBirth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MonthOfBirth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("INTEGER");

                    b.HasKey("BirthdayId");

                    b.ToTable("BirthdayInfo");
                });

            modelBuilder.Entity("BirthdayApp.Database.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BirthdayId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BirthdayApp.Database.Models.BirthdayInfo", b =>
                {
                    b.HasOne("BirthdayApp.Database.Models.User", "User")
                        .WithOne("BirthdayInfo")
                        .HasForeignKey("BirthdayApp.Database.Models.BirthdayInfo", "BirthdayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirthdayApp.Database.Models.User", b =>
                {
                    b.Navigation("BirthdayInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
