﻿// <auto-generated />
using System;
using MeetingRoom.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetingRoom.Core.Migrations
{
    [DbContext(typeof(MeetingRoomContext))]
    [Migration("20230810090306_AddIdentity")]
    partial class AddIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeetingRoom.Core.Models.Auth.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Auth.UserAuth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.CompanyProfile", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("description")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("email")
                        .IsFixedLength();

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("image")
                        .HasColumnName("logo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("CompanyProfile", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfMeeting")
                        .HasColumnType("date")
                        .HasColumnName("dateOfMeeting");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("date")
                        .HasColumnName("endTime");

                    b.Property<bool>("MeetingStatus")
                        .HasColumnType("bit")
                        .HasColumnName("meetingStatus");

                    b.Property<int>("NbOfAttendees")
                        .HasColumnType("int")
                        .HasColumnName("nbOfAttendees");

                    b.Property<int>("RelatedRoom")
                        .HasColumnType("int")
                        .HasColumnName("relatedRoom");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("date")
                        .HasColumnName("startTime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RelatedRoom");

                    b.HasIndex("UserId");

                    b.ToTable("Meeting", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasColumnName("capacity");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("description")
                        .IsFixedLength();

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("location")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.Property<int>("RelatedCompany")
                        .HasColumnType("int")
                        .HasColumnName("relatedCompany");

                    b.HasKey("Id");

                    b.HasIndex("RelatedCompany");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("age");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("companyId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
                        .HasColumnName("email")
                        .IsFixedLength();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .HasColumnName("firstName")
                        .IsFixedLength();

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("gender")
                        .IsFixedLength();

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .HasColumnName("lastName")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("char(15)")
                        .HasColumnName("password")
                        .IsFixedLength();

                    b.Property<string>("PhoneNb")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .HasColumnName("phoneNb")
                        .IsFixedLength();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("char(15)")
                        .HasColumnName("username")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Meeting", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Room", "RelatedRoomNavigation")
                        .WithMany("Meetings")
                        .HasForeignKey("RelatedRoom")
                        .IsRequired()
                        .HasConstraintName("FK_Meeting_Room");

                    b.HasOne("MeetingRoom.Core.Models.User", "User")
                        .WithMany("Meetings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Meeting_User");

                    b.Navigation("RelatedRoomNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Room", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.CompanyProfile", "RelatedCompanyNavigation")
                        .WithMany("Rooms")
                        .HasForeignKey("RelatedCompany")
                        .IsRequired()
                        .HasConstraintName("FK_Room_CompanyProfile");

                    b.Navigation("RelatedCompanyNavigation");
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.User", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.CompanyProfile", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .IsRequired()
                        .HasConstraintName("FK_User_CompanyProfile");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Auth.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Auth.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Auth.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Auth.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeetingRoom.Core.Models.Auth.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MeetingRoom.Core.Models.Auth.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.CompanyProfile", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.Room", b =>
                {
                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("MeetingRoom.Core.Models.User", b =>
                {
                    b.Navigation("Meetings");
                });
#pragma warning restore 612, 618
        }
    }
}
