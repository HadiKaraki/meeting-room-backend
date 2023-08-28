using AutoMapper;
using MeetingRoom.Api.Resources;
using MeetingRoom.Core.Models.Auth;

namespace MeetingRoom.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserSignUpResource, UserAuth>()
			.ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));


		}
	}
}
