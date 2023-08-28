using MeetingRoom.Core.Models;

namespace MeetingRoom.Api.Resources
{
	public class UserSignUpResource // here you put all the properties u need to get from express
	{
		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string PhoneNumber { get; set; }

		public string UserName { get; set; }

		public int Age { get; set; }

		public string Role { get; set; }

		public int CompanyId { get; set; }

		public virtual CompanyProfile Company { get; set; } = null!;

		public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

	}
}