﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication.Models;

namespace WebApplication.Migrations
{
    [DbContext(typeof(StudentsContext))]
    [Migration("20211009082912_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("WebApplication.Models.StudentsGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<string>("Department")
                        .HasColumnType("text");

                    b.Property<string>("GraduatingDepartment")
                        .HasColumnType("text");

                    b.Property<string>("GroupLeaderFullName")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<int>("StudentsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("StudentsGroups");
                });
#pragma warning restore 612, 618
        }
    }
}