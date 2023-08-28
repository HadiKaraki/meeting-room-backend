using MeetingRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Services.Interfaces
{
	public interface ICompanyProfileService
	{
		Task<IEnumerable<CompanyProfile>> GetAllCompanyProfiles();
		Task<CompanyProfile> GetCompanyProfileById(int id);
		Task<CompanyProfile> CreateCompanyProfile(CompanyProfile companyProfile);
		void UpdateCompanyProfile(CompanyProfile company);
		Task<CompanyProfile> DeleteCompanyProfile(CompanyProfile companyProfile);
		void AddRoom(CompanyProfile companyProfile, Room room);
	}
}
