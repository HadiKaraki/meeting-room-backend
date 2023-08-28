using Microsoft.AspNetCore.Identity;

namespace MeetingRoom.Core.Models.Auth
{
	// here you ONLY put the properties that u ADDED to AspNetUser table. everything else like UserName, PhoneNumber etc are already available by the inheritence of IdentityUser.
	public class UserAuth : IdentityUser<Guid>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public int CompanyId { get; set; }

		public virtual CompanyProfile Company { get; set; }

		//public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
	}
}