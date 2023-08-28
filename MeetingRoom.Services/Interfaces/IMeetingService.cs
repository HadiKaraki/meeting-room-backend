using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Services.Interfaces
{
	public interface IMeetingService
	{
		Task<IEnumerable<Meeting>> GetAllMeetings();
		Task<Meeting> GetMeetingById(int id);
		Task<Meeting> CreateMeeting(Meeting Meeting);
		Task<Meeting> UpdateMeeting(Meeting MeetingToBeUpdated, Meeting Meeting);
		Task<Meeting> DeleteMeeting(Meeting Meeting);
		Task<Meeting> GetMeetingByStartTime(string startTime, string dateOfStart);
		List<Meeting> GetMeetingsByRoomId(int roomId);
	}
}
