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
	public class RoomRepository : Repository<Room>, IRoomRepository
	{
		public RoomRepository(MeetingRoomContext context)
			: base(context)
		{ }

		public async Task<IEnumerable<Room>> GetAllRoomsAsync()
		{
			return await MyDbContext.Rooms
				.ToListAsync();
		}

		public Task<Room> GetRoomByIdAsync(int id)
		{
			return MyDbContext.Rooms
				.Include(a => a.RelatedCompanyNavigation)
				.Include(a => a.Meetings)
				.SingleOrDefaultAsync(a => a.Id == id);
		}

		private MeetingRoomContext MyDbContext
		{
			get { return Context as MeetingRoomContext; }
		}
	}
}
