using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MeetingRoom.Api.Resources;
using MeetingRoom.Api.Settings;
using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using MeetingRoom.Core.Models.Auth;
using MeetingRoom.Resources;
using MeetingRoom.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetingRoom.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<UserAuth> _userManager;
		private readonly IMapper _mapper;
		private readonly RoleManager<Role> _roleManager;
		private readonly JwtSettings _jwtSettings;
		private readonly MeetingRoomContext _context;
		private readonly ICompanyProfileService _companyProfileService;
		private readonly IUnitOfWork _unitOfWork;

		public AuthController(
			IMapper mapper,
			UserManager<UserAuth> userManager,
			RoleManager<Role> roleManager,
			IOptionsSnapshot<JwtSettings> jwtSettings,
			MeetingRoomContext context,
			ICompanyProfileService companyProfileService, 
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtSettings = jwtSettings.Value;
			_context = context;
			_companyProfileService = companyProfileService;
			_unitOfWork = unitOfWork;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
		{
			var user = _mapper.Map<UserSignUpResource, UserAuth>(userSignUpResource);

			user.UserName = userSignUpResource.UserName;

			var company = await _context.CompanyProfiles.FindAsync(userSignUpResource.CompanyId);

			if (company == null)
			{
				return NotFound();
			}

			user.Company = company;

			var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

			if (userCreateResult.Succeeded)
			{
				// Add the new user to the Company's Users collection
				company.Users.Add(user);

				//await _context.SaveChangesAsync();

				var savedUser = await GetUserByEmail(user.Email);

				await _userManager.AddToRoleAsync(savedUser, userSignUpResource.Role);

				await _context.SaveChangesAsync();

				return Created(string.Empty, string.Empty);
			}

			return Problem(userCreateResult.Errors.First().Description, null, 500);
		}

		[EnableCors("AllowOrigin")]
		[HttpPost("SignIn")]
		public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
		{
			var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.UserName);
			if (user is null)
			{
				return NotFound("User not found");
			}

			var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

			if (userSigninResult)
			{
				var roles = await _userManager.GetRolesAsync(user);
				return Ok(GenerateJwt(user, roles));
			}

			return BadRequest("Email or password incorrect.");
		}

		[HttpGet("/User/{username}/Role")]
		public async Task<IActionResult> GetUserRole(string username)
		{
			var user = await GetUserByUsername(username);
			var roles = await _userManager.GetRolesAsync(user);
			return Ok(roles);
		}

		[HttpPost("Roles")]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			if (string.IsNullOrWhiteSpace(roleName))
			{
				return BadRequest("Role name should be provided.");
			}

			var newRole = new Role
			{
				Name = roleName
			};

			var roleResult = await _roleManager.CreateAsync(newRole);

			if (roleResult.Succeeded)
			{
				return Ok();
			}

			return Problem(roleResult.Errors.First().Description, null, 500);
		}

		[HttpPost("/User/{userEmail}/Role")]
		public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
		{
			var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

			var result = await _userManager.AddToRoleAsync(user, roleName);

			if (result.Succeeded)
			{
				return Ok();
			}

			return Problem(result.Errors.First().Description, null, 500);
		}

		[EnableCors("AllowOrigin")]
		[HttpGet("/User/Email/{email}")]
		public async Task<UserAuth> GetUserByEmail(string email)
		{
			var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
			if (user is null)
			{
				return null;
			}

			return user;

		}

		[EnableCors("AllowOrigin")]
		[HttpGet("/User/All")]
		public async Task<IActionResult> GetAllUsers()
		{
			return Ok(_userManager.Users);

		}

		[EnableCors("AllowOrigin")]
		[HttpGet("/User/Id/{id}")]
		public async Task<UserAuth> GetUserById(string id)
		{
			Guid guid = Guid.Parse(id);
			var user = _userManager.Users.SingleOrDefault(u => u.Id == guid);
			if (user is null)
			{
				return null;
			}

			return user;

		}

		[EnableCors("AllowOrigin")]
		[HttpGet("/User/Username/{username}")]
		public async Task<UserAuth> GetUserByUsername(string username)
		{
			var user = _context.Users
					   .Include(u => u.Company)
					   //.Include(u => u.Meetings)
					   .SingleOrDefault(u => u.UserName == username);
			//var companyId = user.CompanyId;
			//var company = await _companyProfileService.GetCompanyProfileById(companyId);
			//user.Company = company;
			if (user is null)
			{
				return null;
			}

			return user;

		}

		[EnableCors("AllowOrigin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, UserAuth updatedUser)
		{
			if (id != updatedUser.Id)
			{
				return BadRequest();
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			user.FirstName = updatedUser.FirstName;
			user.LastName = updatedUser.LastName;
			user.Age = updatedUser.Age;
			user.Email = updatedUser.Email;
			user.UserName = updatedUser.UserName;
			//user.CompanyId = updatedUser.CompanyId;

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				return StatusCode(500, result.Errors);
			}

			return NoContent();
		}

		[EnableCors("AllowOrigin")]
		[HttpDelete("/User/{id}")]
		public async Task<UserAuth> DeleteUserById(string id)
		{
			Guid guid = Guid.Parse(id);
			var user = _userManager.Users.SingleOrDefault(u => u.Id == guid);
			if (user is null)
			{
				return null;
			}
			await _userManager.DeleteAsync(user);

			return user;

		}

		[HttpGet("getAllRoles")]
		public async Task<IActionResult> GetAllRoles()
		{
			return Ok(_roleManager.Roles);
		}

		[HttpGet("/Role/{name}")]
		public async Task<IActionResult> GetRoleByName(string name)
		{
			Role role = await _roleManager.FindByNameAsync(name);
			return Ok(role);
		}

		[HttpDelete("/Role/{id}")]
		public async Task<IActionResult> DeleteRoleById(string id)
		{
			Role role = await _roleManager.FindByIdAsync(id);
			await _roleManager.DeleteAsync(role);
			return NoContent();
		}

		private string GenerateJwt(UserAuth user, IList<string> roles)
		{

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
			claims.AddRange(roleClaims);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Issuer,
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}