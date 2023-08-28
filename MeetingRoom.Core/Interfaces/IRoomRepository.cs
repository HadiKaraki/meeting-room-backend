using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Interfaces
{
	public interface IRoomRepository : IRepository<Room>
	{
		Task AddAsync(Room newRoom);
		Task<IEnumerable<Room>> GetAllRoomsAsync();
		Task<Room> GetRoomByIdAsync(int id);
		void Remove(Room oldRoom);
	}
}
