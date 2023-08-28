using MeetingRoom.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeetingRoom.Core.Models;

public partial class Meeting
{
	public int Id { get; set; }

	public System.Guid UserId { get; set; }

	public int RelatedRoom { get; set; }

	public string DateOfStart { get; set; }

	public string DateOfEnd { get; set; }

	public string Title { get; set; }

	public string StartTime { get; set; }

	public string EndTime { get; set; }

	public int NbOfAttendees { get; set; }

	public bool MeetingStatus { get; set; }

	public virtual Room RelatedRoomNavigation { get; set; } = null!;

	//public virtual UserAuth User { get; set; } = null!;

}
