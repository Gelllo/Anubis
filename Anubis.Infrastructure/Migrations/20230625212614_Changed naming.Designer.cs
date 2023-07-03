﻿// <auto-generated />
using System;
using Anubis.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anubis.Infrastructure.Migrations
{
    [DbContext(typeof(AnubisContext))]
    [Migration("20230625212614_Changed naming")]
    partial class Changednaming
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Anubis.Domain.UsersDomain.MedicPatient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MedicID")
                        .HasColumnType("int")
                        .HasColumnName("Medic");

                    b.Property<int>("PatientID")
                        .HasColumnType("int")
                        .HasColumnName("Patient");

                    b.HasKey("Id");

                    b.HasIndex("MedicID");

                    b.HasIndex("PatientID");

                    b.ToTable("MedicPatient", (string)null);
                });

            modelBuilder.Entity("Anubis.Domain.UsersDomain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("LastName");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Role");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Anubis.Domain.UsersDomain.UserRefreshToken", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("UserID");

                    b.Property<DateTime>("Created")
                        .HasColumnType("DateTime")
                        .HasColumnName("Created");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("DateTime")
                        .HasColumnName("Expires");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Token");

                    b.HasKey("UserID");

                    b.ToTable("UserRefreshTokens", (string)null);
                });

            modelBuilder.Entity("Anubis.Domain.UsersDomain.MedicPatient", b =>
                {
                    b.HasOne("Anubis.Domain.UsersDomain.User", "Medic")
                        .WithMany()
                        .HasForeignKey("MedicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Anubis.Domain.UsersDomain.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Medic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Anubis.Domain.UsersDomain.UserRefreshToken", b =>
                {
                    b.HasOne("Anubis.Domain.UsersDomain.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Anubis.Domain.UsersDomain.UserRefreshToken", "UserID")
                        .HasPrincipalKey("Anubis.Domain.UsersDomain.User", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Anubis.Domain.UsersDomain.User", b =>
                {
                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}
