﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vidya.Infrastructure.Data;

#nullable disable

namespace Vidya.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250209071532_InitialCollegeAdd")]
    partial class InitialCollegeAdd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vidya.Domain.Entities.College", b =>
                {
                    b.Property<int>("CollegeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollegeId"));

                    b.Property<DateTime?>("AddOnDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("AuthAdd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthDel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthLstEdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollegeName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("DelOnDt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DelStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EditOnDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("CollegeId");

                    b.ToTable("College");
                });

            modelBuilder.Entity("Vidya.Domain.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateTime?>("AddOnDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adhaar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthAdd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthDel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthLstEdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodGrp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Caste")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DelOnDt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DelStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EditOnDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FathersEmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FathersMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FathersName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MothersEmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MothersMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MothersName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PINCode")
                        .HasColumnType("int");

                    b.Property<string>("Per_Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Per_City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Per_PINCode")
                        .HasColumnType("int");

                    b.Property<string>("Per_States")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhysicalDisability")
                        .HasColumnType("int");

                    b.Property<string>("Religion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("States")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("student");
                });

            modelBuilder.Entity("Vidya.Domain.Entities.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("AddOnDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("AuthAdd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthDel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthLstEdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DelOnDt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DelStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EditOnDt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsAdmin")
                        .HasColumnType("int");

                    b.Property<int>("MasterID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("tbluser");
                });
#pragma warning restore 612, 618
        }
    }
}
