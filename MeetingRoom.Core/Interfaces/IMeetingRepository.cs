using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Interfaces
{
	public interface IMeetingRepository : IRepository<Meeting>
	{
		Task AddAsync(Meeting newMeeting);
		Task<IEnumerable<Meeting>> GetAllMeetingsAsync();
		Task<Meeting> GetMeetingByIdAsync(int id);
		void Remove(Meeting oldMeeting);
	}
}
