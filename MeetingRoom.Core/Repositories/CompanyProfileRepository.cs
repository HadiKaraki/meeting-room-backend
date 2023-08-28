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
	public class CompanyProfileRepository : Repository<CompanyProfile>, ICompanyProfileRepository
	{
		public CompanyProfileRepository(MeetingRoomContext context)
			: base(context)
		{ }

		public async Task<IEnumerable<CompanyProfile>> GetAllCompanyProfilesAsync()
		{
			return await MyDbContext.CompanyProfiles
				.ToListAsync();
		}

		public Task<CompanyProfile> GetCompanyProfileByIdAsync(int id)
		{
			return MyDbContext.CompanyProfiles
				.Include(a => a.Users)
				.Include(a => a.Rooms)
				.SingleOrDefaultAsync(a => a.Id == id);
		}

		private MeetingRoomContext MyDbContext
		{
			get { return Context as MeetingRoomContext; }
		}
	}
}
