using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MeetingRoomContext _context;
		private RoomRepository _roomRepository;
		private CompanyProfileRepository _companyProfileRepository;
		private MeetingRepository _meetingRepository;

		public UnitOfWork(MeetingRoomContext context)
		{
			this._context = context;
		}

		public IRoomRepository Rooms => _roomRepository = _roomRepository ?? new RoomRepository(_context);
		public IMeetingRepository Meetings => _meetingRepository = _meetingRepository ?? new MeetingRepository(_context);
		public ICompanyProfileRepository CompanyProfiles => _companyProfileRepository = _companyProfileRepository ?? new CompanyProfileRepository(_context);

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
