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
	public class CompanyProfileService : ICompanyProfileService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MeetingRoomContext _context;
		public CompanyProfileService(IUnitOfWork unitOfWork, MeetingRoomContext context)
		{
			this._unitOfWork = unitOfWork;
			_context = context;
		}

		public async Task<CompanyProfile> CreateCompanyProfile(CompanyProfile newCompanyProfile)
		{
			await _unitOfWork.CompanyProfiles.AddAsync(newCompanyProfile);
			await _unitOfWork.CommitAsync();
			return newCompanyProfile;
		}

		public async Task<CompanyProfile> DeleteCompanyProfile(CompanyProfile oldCompanyProfile)
		{
			_unitOfWork.CompanyProfiles.Remove(oldCompanyProfile);
			await _unitOfWork.CommitAsync();
			return oldCompanyProfile;
		}

		public void UpdateCompanyProfile(CompanyProfile company)
		{
			_context.CompanyProfiles.Update(company);
			_context.SaveChanges();
		}

		public async Task<CompanyProfile> GetCompanyProfileById(int id)
		{
			return await _unitOfWork.CompanyProfiles.GetCompanyProfileByIdAsync(id);
		}

		public async Task<IEnumerable<CompanyProfile>> GetAllCompanyProfiles()
		{
			return await _unitOfWork.CompanyProfiles
				.GetAllCompanyProfilesAsync();
		}

		public void AddRoom(CompanyProfile company, Room room)
		{
			company.Rooms.Add(room);
			_context.CompanyProfiles.Update(company);
			_context.SaveChanges();
		}
	}
}
