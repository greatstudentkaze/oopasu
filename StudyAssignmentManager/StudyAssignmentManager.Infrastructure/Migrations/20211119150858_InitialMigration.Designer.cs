// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudyAssignmentManager.Infrastructure;

namespace StudyAssignmentManager.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20211119150858_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("StudyAssignmentManager.Domain.AssignmentData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("EditorJSData")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AssignmentDatas");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.CheckRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ReviewerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("CheckRequests");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.StudyAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DataId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DataId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TutorId");

                    b.ToTable("StudyAssignments");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<List<Guid>>("RoleIds")
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserUserRole");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.CheckRequest", b =>
                {
                    b.HasOne("StudyAssignmentManager.Domain.StudyAssignment", "Assignment")
                        .WithMany("CheckRequests")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyAssignmentManager.Domain.User", "Reviewer")
                        .WithMany("CheckRequests")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.StudyAssignment", b =>
                {
                    b.HasOne("StudyAssignmentManager.Domain.AssignmentData", "Data")
                        .WithMany("Assignments")
                        .HasForeignKey("DataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyAssignmentManager.Domain.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyAssignmentManager.Domain.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyAssignmentManager.Domain.User", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Data");

                    b.Navigation("Student");

                    b.Navigation("Teacher");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.HasOne("StudyAssignmentManager.Domain.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyAssignmentManager.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.AssignmentData", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.StudyAssignment", b =>
                {
                    b.Navigation("CheckRequests");
                });

            modelBuilder.Entity("StudyAssignmentManager.Domain.User", b =>
                {
                    b.Navigation("CheckRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
