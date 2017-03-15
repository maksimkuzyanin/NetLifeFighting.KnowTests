using AutoMapper;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.Web.DTO.Test;

namespace NetLifeFighting.KnowTests.Mapping.Configuration.Profiles
{
	public class TestsProfile: Profile
	{
		protected override void Configure()
		{


			CreateMap<Test, TestDto>()
				.ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.MaxTime, opt => opt.MapFrom(src => src.MaxTime))
				.ForMember(dest => dest.LevelOfDifficulty, opt => opt.MapFrom(src => src.LevelOfDifficultyStr))
				.ForMember(dest => dest.RelevanceStatus, opt => opt.MapFrom(src => src.RelevanceStatusStr))
				.ForMember(dest => dest.RelevanceDate, opt => opt.MapFrom(src => src.RelevanceDate));

			// todo: почистить ненужный мапинг, удалить ненужные дто'шки

			/*CreateMap<Test, TestDto>()
				.ForMember(dest => dest.TestInfo.TestId, opt => opt.MapFrom(src => src.TestId))
				.ForMember(dest => dest.TestInfo.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.TestInfo.MaxTime, opt => opt.MapFrom(src => src.MaxTime))
				.ForMember(dest => dest.TestInfo.LevelOfDifficulty, opt => opt.MapFrom(src => src.LevelOfDifficultyStr))
				.ForMember(dest => dest.TestInfo.RelevanceStatus, opt => opt.MapFrom(src => src.RelevanceStatusStr))
				.ForMember(dest => dest.TestInfo.RelevanceDate, opt => opt.MapFrom(src => src.RelevanceDate))
				.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.TestQuestions));*/

			CreateMap<TestQuestion, TestDto>()
				.ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Test.Title))
				.ForMember(dest => dest.MaxTime, opt => opt.MapFrom(src => src.MaxTime))
				.ForMember(dest => dest.LevelOfDifficulty, opt => opt.MapFrom(src => src.Test.LevelOfDifficultyStr))
				.ForMember(dest => dest.RelevanceStatus, opt => opt.MapFrom(src => src.Test.RelevanceStatusStr))
				.ForMember(dest => dest.RelevanceDate, opt => opt.MapFrom(src => src.Test.RelevanceDate));

			CreateMap<TestQuestion, QuestionDto>()
				.ForMember(dest => dest.Questid, opt => opt.MapFrom(src => src.QuestId))
				.ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
				.ForMember(dest => dest.GroupQuestId, opt => opt.MapFrom(src => src.Question.GroupQuestId))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Question.Title))
				.ForMember(dest => dest.Literal, opt => opt.MapFrom(src => src.Question.Literal))
				.ForMember(dest => dest.LevelOfDifficulty, opt => opt.MapFrom(src => src.Question.LevelOfDifficultyStr))
				.ForMember(dest => dest.AnswerType, opt => opt.MapFrom(src => src.Question.AnswerTypeStr))
				.ForMember(dest => dest.QuestNum, opt => opt.MapFrom(src => src.QuestNum))
				.ForMember(dest => dest.MaxTime, opt => opt.MapFrom(src => src.MaxTime));
				//.ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Question.QuestsAnswers));

			CreateMap<QuestAnswer, AnswerDto>()
				.ForMember(dest => dest.AnswerId, opt => opt.MapFrom(src => src.AnswerId))
				.ForMember(dest => dest.QuestId, opt => opt.MapFrom(src => src.QuestId))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Answer.Title))
				.ForMember(dest => dest.Literal, opt => opt.MapFrom(src => src.Answer.Literal))
				.ForMember(dest => dest.IsRight, opt => opt.MapFrom(src => src.IsRight))
				.ForMember(dest => dest.PriorityNo, opt => opt.MapFrom(src => src.PriorityNo))
				.ForMember(dest => dest.GroupNum, opt => opt.MapFrom(src => src.GroupNum));
		}
	}
}
