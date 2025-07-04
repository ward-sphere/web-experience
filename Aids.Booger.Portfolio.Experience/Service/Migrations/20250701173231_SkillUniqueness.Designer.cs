﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Infrastructure;

#nullable disable

namespace Service.Migrations
{
    [DbContext(typeof(ExperienceContext))]
    [Migration("20250701173231_SkillUniqueness")]
    partial class SkillUniqueness
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Model.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Field")
                        .HasColumnType("text");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("Domain.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Domain.Model.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmploymentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Work");
                });

            modelBuilder.Entity("Domain.Model.WorkAchievement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WorkId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkAchievement");
                });

            modelBuilder.Entity("Domain.Model.WorkSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("WorkId", "SkillId")
                        .IsUnique();

                    b.ToTable("WorkSkill");
                });

            modelBuilder.Entity("Domain.Model.WorkAchievement", b =>
                {
                    b.HasOne("Domain.Model.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("Domain.Model.WorkSkill", b =>
                {
                    b.HasOne("Domain.Model.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Model.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Work");
                });
#pragma warning restore 612, 618
        }
    }
}
