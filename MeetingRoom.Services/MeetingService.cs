using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using MeetingRoom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeetingRoom.Core.Models.Auth;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeetingRoom.Services
{
	public class MeetingService : IMeetingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MeetingRoomContext _context;
		private readonly UserManager<UserAuth> _userManager;
		public MeetingService(IUnitOfWork unitOfWork, MeetingRoomContext context, UserManager<UserAuth> userManager)
		{
			this._unitOfWork = unitOfWork;
			_context = context;
			_userManager = userManager;
		}

		public async Task<Meeting> CreateMeeting(Meeting newMeeting)
		{
			var room = await _context.Rooms.FindAsync(newMeeting.RelatedRoom);

			newMeeting.RelatedRoomNavigation = room;

			room.Meetings.Add(newMeeting);

			await _unitOfWork.Meetings.AddAsync(newMeeting);

			await _unitOfWork.CommitAsync();

			return newMeeting;
		}

		public async Task<Meeting> DeleteMeeting(Meeting oldMeeting)
		{
			_unitOfWork.Meetings.Remove(oldMeeting);
			await _unitOfWork.CommitAsync();
			return oldMeeting;
		}

		public async Task<Meeting> UpdateMeeting(Meeting MeetingToBeUpdated, Meeting Meeting)
		{
			MeetingToBeUpdated.NbOfAttendees = Meeting.NbOfAttendees;
			MeetingToBeUpdated.Title = Meeting.Title;
			MeetingToBeUpdated.StartTime = Meeting.StartTime;
			MeetingToBeUpdated.EndTime = Meeting.EndTime;
			MeetingToBeUpdated.DateOfEnd = Meeting.DateOfEnd;
			MeetingToBeUpdated.DateOfStart = Meeting.DateOfStart;

			await _unitOfWork.CommitAsync();

			return MeetingToBeUpdated;
		}

		public async Task<Meeting> GetMeetingById(int id)
		{
			return await _unitOfWork.Meetings.GetMeetingByIdAsync(id);
		}

		public async Task<IEnumerable<Meeting>> GetAllMeetings()
		{
			return await _unitOfWork.Meetings
				.GetAllMeetingsAsync();
		}
		public async Task<Meeting> GetMeetingByStartTime(string startTime, string dateOfStart)
		{
			var meeting = _context.Meetings.FirstOrDefault(u => u.StartTime == startTime && u.DateOfStart == dateOfStart);

			return meeting;
		}

		public List<Meeting> GetMeetingsByRoomId(int roomId)
		{
			var meetingsInRoom = _context.Meetings
										 .Where(meeting => meeting.RelatedRoom == roomId)
										 .ToList();
			return meetingsInRoom;
		}
	}
}
