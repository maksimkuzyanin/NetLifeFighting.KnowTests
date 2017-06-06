using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using NetLifeFighting.KnowTests.Common.Abstraction.Result;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Tests;
using NetLifeFighting.KnowTests.Web.DTO.Person;
using NetLifeFighting.KnowTests.Web.DTO.Test;
using NetLifeFighting.KnowTests.Web.Helpers;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	[RoutePrefix("app/api/tests")]
	public class TestsController : ApiController
	{
		private readonly TestDao _testDao;
		private readonly QuestionDao _questionDao;
		private readonly PersonDao _personDao;

		public TestsController()
		{
			_testDao = new TestDao();
			_questionDao = new QuestionDao();
			_personDao = new PersonDao();
		}

		[HttpPost]
		[Route]
		public Result<PreparedTestDto[]> GetPreparedTests()
		{
			var preparedTests = PreparedPersonsTestsDataLocator.Instance.GetPreparedTests().ToArray();

			return new Result<PreparedTestDto[]>(preparedTests);
		}

		[HttpPost]
		[Route("{testId}")]
		public Result<PreparedTestDto> GetPreparedTest(int testId)
		{
			var preparedTest = PreparedPersonsTestsDataLocator.Instance.GetPreparedTest(testId);

			return new Result<PreparedTestDto>(preparedTest);
		}

		[HttpGet]
		[Route("results/{personId}")]
		public Result<PersonTestResultDto[]> GetPersonTestResults(int personId)
		{
			var personTestsResults = PreparedPersonsTestsDataLocator.Instance.GetPersonTestsResults(personId).ToArray();

			return new Result<PersonTestResultDto[]>(personTestsResults);
		}

		[HttpPost]
		[Route("result")]
		public Result<PersonTestResultDto> GetPersonTestResult([FromBody]IEnumerable<PersonAnswerDto> personAnswers)
		{
			// приводим дто-объекты к сущности персоны
			PersonAnswer[] getPersonAnswers = personAnswers.Select(GetPersonAnswer).ToArray();

			var personTestResult = PreparedPersonsTestsDataLocator.Instance.GetPersonTestResult(getPersonAnswers);

			return new Result<PersonTestResultDto>(personTestResult);
		}

		[HttpPost]
		[Route("result/save")]
		public void SaveTestResult(PersonAnswerDto[] personAnswers)
		{
			// приводим дто-объекты к сущности персоны
			PersonAnswer[] setPersonAnswers = personAnswers.Select(GetPersonAnswerShort).ToArray();

			// идентификатор персоны
			var personId = setPersonAnswers.First().Data.PersonId;

			// идентификатор теста
			var testId = setPersonAnswers.First().Data.TestId;

			// сохранить результат в базу
			_personDao.SaveTestResult(personId, testId, setPersonAnswers);
		}

		[HttpGet]
		[Route("approve/{currQuestId}")]
		public Result<AnswerDto[]> ApproveQuesResult(int currQuestId)
		{
			// правильные ответы
			var currQuest = _questionDao.GetById(currQuestId);
			var rightAnswers = currQuest.QuestsAnswers.Where(x => x.IsRight);
			
			// приводится к дтошке
			var answersDto = rightAnswers.Select(Mapper.Map<AnswerDto>).ToArray();
			return new Result<AnswerDto[]>(answersDto);
		}

		private PersonAnswer GetPersonAnswerShort(PersonAnswerDto personAnswer)
		{
			return new PersonAnswer
			{
				Data = new TestData
				{
					PersonId = personAnswer.PersonId,
					TestId = personAnswer.TestId
				},
				QuestId = personAnswer.QuestId,
				AnswerId = personAnswer.AnswerId,
				PriorityNo = personAnswer.PriorityNo
			};
		}

		private PersonAnswer GetPersonAnswer(PersonAnswerDto personAnswer)
		{
			var getPersonAnswer = GetPersonAnswerShort(personAnswer);

			getPersonAnswer.Question = new Question { GroupQuestId = personAnswer.GroupQuestId };

			return getPersonAnswer;
		}
	}
}
