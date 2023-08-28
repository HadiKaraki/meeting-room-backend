using System;
using System.Collections.Generic;
using MeetingRoom.Core.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MeetingRoom.Core.Models;

public partial class MeetingRoomContext : IdentityDbContext<UserAuth, Role, Guid>
{
	public MeetingRoomContext()
	{
	}

	public MeetingRoomContext(DbContextOptions<MeetingRoomContext> options)
		: base(options)
	{
	}

	public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }

	public virtual DbSet<Meeting> Meetings { get; set; }

	public virtual DbSet<Room> Rooms { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Server=DESKTOP-7A45LOD;Database=MeetingRoom;Trusted_Connection=True;Encrypt=False;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CompanyProfile>(entity =>
		{
			base.OnModelCreating(modelBuilder);
			entity.ToTable("CompanyProfile");

			entity.Property(e => e.Id)
				.ValueGeneratedNever()
				.HasColumnName("id");
			entity.Property(e => e.Active).HasColumnName("active");
			entity.Property(e => e.Description)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("description");
			entity.Property(e => e.Email)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("email");
			entity.Property(e => e.Logo)
				.HasColumnType("image")
				.HasColumnName("logo");
			entity.Property(e => e.Name)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("name");
		});

		modelBuilder.Entity<Meeting>(entity =>
		{
			entity.ToTable("Meeting");

			entity.Property(e => e.Id)
				.ValueGeneratedNever()
				.HasColumnName("id");
			entity.Property(e => e.DateOfStart)
				.HasMaxLength(10)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("dateOfStart");
			entity.Property(e => e.DateOfEnd)
				.HasMaxLength(10)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("dateOfEnd");
			entity.Property(e => e.EndTime)
				.HasMaxLength(8)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("endTime");
			entity.Property(e => e.MeetingStatus).HasColumnName("meetingStatus");
			entity.Property(e => e.NbOfAttendees).HasColumnName("nbOfAttendees");
			entity.Property(e => e.RelatedRoom).HasColumnName("relatedRoom");
			entity.Property(e => e.StartTime)
				.HasMaxLength(8)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("startTime");

			entity.HasOne(d => d.RelatedRoomNavigation).WithMany(p => p.Meetings)
				.HasForeignKey(d => d.RelatedRoom)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Meeting_Room");
		});

		modelBuilder.Entity<Room>(entity =>
		{
			entity.ToTable("Room");

			entity.Property(e => e.Id)
				.ValueGeneratedNever()
				.HasColumnName("id");
			entity.Property(e => e.Capacity).HasColumnName("capacity");
			entity.Property(e => e.Description)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("description");
			entity.Property(e => e.Location)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("location");
			entity.Property(e => e.Name)
				.HasMaxLength(50)
				.IsUnicode(false)
				.IsFixedLength()
				.HasColumnName("name");
			entity.Property(e => e.RelatedCompany).HasColumnName("relatedCompany");

			entity.HasOne(d => d.RelatedCompanyNavigation).WithMany(p => p.Rooms)
				.HasForeignKey(d => d.RelatedCompany)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Room_CompanyProfile");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
