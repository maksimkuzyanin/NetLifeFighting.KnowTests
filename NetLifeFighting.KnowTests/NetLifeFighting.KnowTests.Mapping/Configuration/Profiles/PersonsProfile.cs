using AutoMapper;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.Web.DTO.Person;

namespace NetLifeFighting.KnowTests.Mapping.Configuration.Profiles
{
	public class PersonsProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Person, PersonDto>()
				.ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
				.ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
				.ForMember(dest => dest.PersonAnswers, opt => opt.MapFrom(src => src.PersonAnswers));

			CreateMap<PersonAnswer, PersonAnswerDto>()
				.ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
				.ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
				.ForMember(dest => dest.QuestId, opt => opt.MapFrom(src => src.QuestId))
				.ForMember(dest => dest.AnswerId, opt => opt.MapFrom(src => src.AnswerId))
				.ForMember(dest => dest.PriorityNo, opt => opt.MapFrom(src => src.PriorityNo));
		}
	}
}
