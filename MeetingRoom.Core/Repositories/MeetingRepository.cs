using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Repositories
{
	public class MeetingRepository : Repository<Meeting>, IMeetingRepository
	{
		public MeetingRepository(MeetingRoomContext context)
			: base(context)
		{ }

		public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync()
		{
			return await MyDbContext.Meetings
				.ToListAsync();
		}

		public Task<Meeting> GetMeetingByIdAsync(int id)
		{
			return MyDbContext.Meetings
				.Include(a => a.RelatedRoomNavigation)
				.SingleOrDefaultAsync(a => a.Id == id);
		}

		private MeetingRoomContext MyDbContext
		{
			get { return Context as MeetingRoomContext; }
		}
	}
}
