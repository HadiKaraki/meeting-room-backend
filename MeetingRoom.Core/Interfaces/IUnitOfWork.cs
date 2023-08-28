using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IRoomRepository Rooms { get; }
		ICompanyProfileRepository CompanyProfiles { get; }
		IMeetingRepository Meetings { get; }

		Task<int> CommitAsync();
	}
}
