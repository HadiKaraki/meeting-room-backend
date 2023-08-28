using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Core.Interfaces
{
	public interface ICompanyProfileRepository : IRepository<CompanyProfile>
	{
		Task AddAsync(CompanyProfile newCompanyProfile);
		Task<IEnumerable<CompanyProfile>> GetAllCompanyProfilesAsync();
		Task<CompanyProfile> GetCompanyProfileByIdAsync(int id);
	}
}
