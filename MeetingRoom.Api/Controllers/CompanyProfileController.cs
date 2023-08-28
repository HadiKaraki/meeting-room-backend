using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Services.Interfaces;
using MeetingRoom.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using MeetingRoom.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeetingRoom.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	[Authorize]
	public class CompanyProfileController : ControllerBase
	{
		private readonly ICompanyProfileService _companyProfileService;
		private readonly MeetingRoomContext _context;
		private readonly IRoomService _roomService;

		public CompanyProfileController(ICompanyProfileService companyService, IRoomService roomService, MeetingRoomContext context)
		{
			this._companyProfileService = companyService;
			this._roomService = roomService;
			_context = context;
		}

		[EnableCors("AllowOrigin")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CompanyProfile>>> GetAllCompanyProfiles()
		{
			var companyProfiles = await _companyProfileService.GetAllCompanyProfiles();
			return Ok(companyProfiles);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyProfile>> GetCompanyProfileById(int id)
		{
			var companyProfiles = await _companyProfileService.GetCompanyProfileById(id);

			return Ok(companyProfiles);
		}

		[HttpPost]
		public async Task<ActionResult<CompanyProfile>> CreateCompanyProfile(CompanyProfile companyProfile)
		{
			var newCompanyProfile = await _companyProfileService.CreateCompanyProfile(companyProfile);

			return Ok(newCompanyProfile);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCompanyProfile(int id, CompanyProfile company)
		{
			if (company == null || id != company.Id)
				return BadRequest();
			
			_companyProfileService.UpdateCompanyProfile(company);

			return NoContent();
		}

		[HttpPut("{RoomId}/{CompanyId}")]
		public async Task<ActionResult<CompanyProfile>> AddRoom(int RoomId, int CompanyId)
		{
			var company = await _companyProfileService.GetCompanyProfileById(CompanyId);
			var room = await _roomService.GetRoomById(RoomId);
			_companyProfileService.AddRoom(company, room);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompanyProfile(int id)
		{
			var companyProfile = await _companyProfileService.GetCompanyProfileById(id);

			await _companyProfileService.DeleteCompanyProfile(companyProfile);

			return NoContent();
		}

	}
}