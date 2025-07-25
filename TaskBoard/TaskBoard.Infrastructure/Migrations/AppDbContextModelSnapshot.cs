﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskBoard.Infrastructure;

#nullable disable

namespace TaskBoard.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "roles", new[] { "administrator", "manager", "user" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "task_status", new[] { "to_work", "in_work", "ready", "done" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskBoard.Domain.Entities.Files", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SprintId")
                        .HasColumnType("integer");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Role", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Roles")
                        .HasColumnType("roles");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Sprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SprintId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("task_status");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.TaskComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SprintId")
                        .HasColumnType("integer");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskComments");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.TaskExecutor", b =>
                {
                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("TaskId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskExecutor");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Files", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Files")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskBoard.Domain.Entities.Tasks", "Tasks")
                        .WithMany("Files")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskBoard.Domain.Entities.User", "User")
                        .WithMany("Files")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Sprint");

                    b.Navigation("Tasks");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Role", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Project", "Project")
                        .WithMany("Roles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskBoard.Domain.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Sprint", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Tasks", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Tasks")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.TaskComment", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Comments")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskBoard.Domain.Entities.Tasks", "Tasks")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskBoard.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Sprint");

                    b.Navigation("Tasks");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.TaskExecutor", b =>
                {
                    b.HasOne("TaskBoard.Domain.Entities.Tasks", "Tasks")
                        .WithMany("Executors")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskBoard.Domain.Entities.User", "User")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tasks");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Project", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Sprint", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.Tasks", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Executors");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("TaskBoard.Domain.Entities.User", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
