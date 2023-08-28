using MeetingRoom.Core.Models.Auth;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Core.Models;

public partial class CompanyProfile
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string Description { get; set; } = null!;

	public string Email { get; set; } = null!;

	public byte[] Logo { get; set; } = null!;

	public bool Active { get; set; }

	public virtual ICollection<Room>? Rooms { get; set; } = new List<Room>();

	public virtual ICollection<UserAuth>? Users { get; set; } = new List<UserAuth>();
}
