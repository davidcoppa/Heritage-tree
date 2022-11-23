﻿// <auto-generated />
using System;
using EventsManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Events.Core.Migrations
{
    [DbContext(typeof(EventsContext))]
    [Migration("20221122065902_tags")]
    partial class tags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Events.Core.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatesId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Events.Core.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Events.Core.Model.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("Events.Core.Model.MediaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MediaType");
                });

            modelBuilder.Entity("Events.Core.Model.States", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("Events.Core.Model.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EventsManager.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("LoccationId")
                        .HasColumnType("int");

                    b.Property<int>("Person1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Person2Id")
                        .HasColumnType("int");

                    b.Property<int?>("Person3Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("LoccationId");

                    b.HasIndex("Person1Id");

                    b.HasIndex("Person2Id");

                    b.HasIndex("Person3Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("EventsManager.Model.EventTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("EventsManager.Model.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("MediaDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MediaDateUploaded")
                        .HasColumnType("datetime2");

                    b.Property<int>("MediaTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("UrlFile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("MediaTypeId");

                    b.HasIndex("PersonId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("EventsManager.Model.ParentPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonFatherId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonMotherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonFatherId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonMotherId");

                    b.ToTable("ParentPerson");
                });

            modelBuilder.Entity("EventsManager.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("PlaceOfBirthId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaceOfDeathId")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaceOfBirthId");

                    b.HasIndex("PlaceOfDeathId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Events.Core.Model.City", b =>
                {
                    b.HasOne("Events.Core.Model.States", null)
                        .WithMany("Cities")
                        .HasForeignKey("StatesId");
                });

            modelBuilder.Entity("Events.Core.Model.States", b =>
                {
                    b.HasOne("Events.Core.Model.Country", null)
                        .WithMany("States")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("EventsManager.Model.Event", b =>
                {
                    b.HasOne("EventsManager.Model.EventTypes", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Events.Core.Model.Country", "Loccation")
                        .WithMany()
                        .HasForeignKey("LoccationId");

                    b.HasOne("EventsManager.Model.Person", "Person1")
                        .WithMany()
                        .HasForeignKey("Person1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsManager.Model.Person", "Person2")
                        .WithMany()
                        .HasForeignKey("Person2Id");

                    b.HasOne("EventsManager.Model.Person", "Person3")
                        .WithMany()
                        .HasForeignKey("Person3Id");

                    b.Navigation("EventType");

                    b.Navigation("Loccation");

                    b.Navigation("Person1");

                    b.Navigation("Person2");

                    b.Navigation("Person3");
                });

            modelBuilder.Entity("EventsManager.Model.Media", b =>
                {
                    b.HasOne("EventsManager.Model.Event", null)
                        .WithMany("Media")
                        .HasForeignKey("EventId");

                    b.HasOne("Events.Core.Model.MediaType", "MediaType")
                        .WithMany()
                        .HasForeignKey("MediaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsManager.Model.Person", null)
                        .WithMany("Photos")
                        .HasForeignKey("PersonId");

                    b.Navigation("MediaType");
                });

            modelBuilder.Entity("EventsManager.Model.ParentPerson", b =>
                {
                    b.HasOne("EventsManager.Model.Person", "PersonFather")
                        .WithMany()
                        .HasForeignKey("PersonFatherId");

                    b.HasOne("EventsManager.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsManager.Model.Person", "PersonMother")
                        .WithMany()
                        .HasForeignKey("PersonMotherId");

                    b.Navigation("Person");

                    b.Navigation("PersonFather");

                    b.Navigation("PersonMother");
                });

            modelBuilder.Entity("EventsManager.Model.Person", b =>
                {
                    b.HasOne("Events.Core.Model.Country", "PlaceOfBirth")
                        .WithMany()
                        .HasForeignKey("PlaceOfBirthId");

                    b.HasOne("Events.Core.Model.Country", "PlaceOfDeath")
                        .WithMany()
                        .HasForeignKey("PlaceOfDeathId");

                    b.Navigation("PlaceOfBirth");

                    b.Navigation("PlaceOfDeath");
                });

            modelBuilder.Entity("Events.Core.Model.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Events.Core.Model.States", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("EventsManager.Model.Event", b =>
                {
                    b.Navigation("Media");
                });

            modelBuilder.Entity("EventsManager.Model.Person", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
