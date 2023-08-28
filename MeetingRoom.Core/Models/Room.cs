using System;
using System.Collections.Generic;

namespace MeetingRoom.Core.Models;

public partial class Room
{
    public int Id { get; set; }

    public int RelatedCompany { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Capacity { get; set; }

    public string Description { get; set; } = null!;

    //public bool ScheduledMeeting { get; set; }

    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

    public virtual CompanyProfile RelatedCompanyNavigation { get; set; } = null!;
}