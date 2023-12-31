﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using case_study_library.Data;

#nullable disable

namespace case_study_library.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20230907134333_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("case_study_library.Data.BarrowHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BarrowEndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("BarrowEndDate");

                    b.Property<DateTime>("BarrowStartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("BarrowStartDate");

                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("BookId");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PersonName");

                    b.Property<string>("PersonSurname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PersonSurname");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Table_BarrowHistory", "public");
                });

            modelBuilder.Entity("case_study_library.Data.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutBook")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("AboutBook");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Author");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("BookName");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ImageLink");

                    b.Property<bool?>("IsAvaliable")
                        .HasColumnType("boolean")
                        .HasColumnName("IsAvaliable");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("PublicationYear")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PublicationYear");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Publisher");

                    b.HasKey("Id");

                    b.ToTable("table_Book", "public");
                });

            modelBuilder.Entity("case_study_library.Data.BarrowHistory", b =>
                {
                    b.HasOne("case_study_library.Data.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}
