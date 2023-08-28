using MeetingRoom.Data;
using MeetingRoom.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MeetingRoom.Core.Interfaces;
using Microsoft.Identity.Client;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeetingRoom.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	[Authorize]
	public class RoomController : ControllerBase
	{
		private readonly IRoomService _RoomService;
		private readonly ICompanyProfileService _companyProfileService;
		private readonly IUnitOfWork _unitOfWork;

		public RoomController(IRoomService RoomService, ICompanyProfileService _companyProfileService, IUnitOfWork _unitOfWork)
		{
			this._RoomService = RoomService;
			this._companyProfileService = _companyProfileService;
			this._unitOfWork = _unitOfWork;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
		{
			var Rooms = await _RoomService.GetAllRooms();
			return Ok(Rooms);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Room>> GetRoomById(int id)
		{
			var Room = await _RoomService.GetRoomById(id);

			return Ok(Room);
		}

		[HttpPost]
		public async Task<ActionResult<CompanyProfile>> CreateRoom(Room Room)
		{
			var newRoom = await _RoomService.CreateRoom(Room);

			return Ok(newRoom);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Room>> UpdateRoom(int id, Room room)
		{

			if (room == null || id != room.Id)
				return BadRequest();

			_RoomService.UpdateRoom(room);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			var oldRoom = await _RoomService.GetRoomById(id);

			await _RoomService.DeleteRoom(oldRoom);

			return NoContent();
		}
	}
}
