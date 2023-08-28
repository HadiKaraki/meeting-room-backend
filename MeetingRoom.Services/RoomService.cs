using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using MeetingRoom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeetingRoom.Services
{
	public class RoomService : IRoomService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICompanyProfileService _companyProfileService;
		private readonly MeetingRoomContext _context;
		public RoomService(IUnitOfWork unitOfWork, ICompanyProfileService companyProfileService, MeetingRoomContext context)
		{
			this._unitOfWork = unitOfWork;
			this._companyProfileService = companyProfileService;
			this._context = context;
		}

		public async Task<Room> CreateRoom(Room newRoom)
		{
			var company = await _context.CompanyProfiles.FindAsync(newRoom.RelatedCompany);

			newRoom.RelatedCompanyNavigation = company;

			company.Rooms.Add(newRoom);

			await _unitOfWork.Rooms.AddAsync(newRoom);

			await _unitOfWork.CommitAsync();

			return newRoom;
		}

		public async Task<Room> DeleteRoom(Room oldRoom)
		{
			_unitOfWork.Rooms.Remove(oldRoom);
			await _unitOfWork.CommitAsync();
			return oldRoom;
		}

		public void UpdateRoom(Room Room)
		{
			_context.Rooms.Update(Room);
			_context.SaveChanges();
		}

		public async Task<Room> GetRoomById(int id)
		{
			return await _unitOfWork.Rooms.GetRoomByIdAsync(id);
		}

		public async Task<IEnumerable<Room>> GetAllRooms()
		{
			return await _unitOfWork.Rooms
				.GetAllRoomsAsync();
		}
	}
}
