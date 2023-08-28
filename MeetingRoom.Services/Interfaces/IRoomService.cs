using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Services.Interfaces
{
	public interface IRoomService
	{
		Task<IEnumerable<Room>> GetAllRooms();
		Task<Room> GetRoomById(int id);
		Task<Room> CreateRoom(Room Room);
		void UpdateRoom(Room Room);
		Task<Room> DeleteRoom(Room Room);
	}
}
