using MeetingRoom.Data;
using MeetingRoom.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeetingRoom.Core.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace MeetingRoom.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	[Authorize]
	public class MeetingController : ControllerBase
	{
		private readonly IMeetingService _meetingService;
		private readonly IRoomService _RoomService;
		private readonly UserManager<UserAuth> _userManager;

		public MeetingController(IMeetingService meetingService, IRoomService roomService, UserManager<UserAuth> userManager)
		{
			this._meetingService = meetingService;
			_RoomService = roomService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Meeting>>> GetAllMeetings()
		{
			var meetings = await _meetingService.GetAllMeetings();
			return Ok(meetings);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Meeting>> GetMeetingById(int id)
		{
			var meeting = await _meetingService.GetMeetingById(id);

			return Ok(meeting);
		}

		[HttpPost]
		public async Task<ActionResult<Meeting>> CreateMeeting(Meeting meeting)
		{
			var newMeeting = await _meetingService.CreateMeeting(meeting);

			return Ok(newMeeting);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Meeting>> UpdateMeeting(int id, Meeting meeting)
		{

			var meetingToBeUpdated = await _meetingService.GetMeetingById(id);

			if (meetingToBeUpdated == null)
				return NotFound();

			var updatedMeeting = await _meetingService.UpdateMeeting(meetingToBeUpdated, meeting);

			return Ok(updatedMeeting);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMeeting(int id)
		{
			var oldMeeting = await _meetingService.GetMeetingById(id);

			await _meetingService.DeleteMeeting(oldMeeting);

			return NoContent();
		}

		[HttpGet("/Meeting/startTime/{startTime}/{dateOfStart}")]
		public async Task<ActionResult<Meeting>> GetMeetingByStartTime(string startTime, string dateOfStart)
		{
			var meeting = await _meetingService.GetMeetingByStartTime(startTime, dateOfStart);

			return Ok(meeting);
		}

		[HttpGet("/Meeting/roomId/{roomId}")]
		public async Task<ActionResult<Meeting>> GetMeetingByStartTime(int roomId)
		{
			var meetings = _meetingService.GetMeetingsByRoomId(roomId);

			return Ok(meetings);
		}
	}
}