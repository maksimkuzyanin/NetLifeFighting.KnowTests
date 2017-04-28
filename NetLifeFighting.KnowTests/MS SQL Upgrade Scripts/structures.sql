SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*USE dev_lf_tests;
GO*/

-- структура тестов

-- тесты
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='Test'
	) begin
	
	create table Test (
		TestId int identity(1,1),
		Title varchar(150) COLLATE Cyrillic_General_CI_AS not null,
		MaxTime int,
		LevelOfDifficulty char(1) not null default('S'),
		RelevanceStatus char(1) not null default('R'),
		RelevanceDate datetime,
		
		CONSTRAINT [PK_Test] PRIMARY KEY (
			TestId ASC
		)
	);
end
GO

-- вопросы
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='Question'
	) begin
	
	/*
		/// <summary>
		/// Тип ответа на вопрос
		/// </summary>
		public enum AnswerType
		{
			/// <summary>
			/// Один ответ
			/// </summary>
			Single = 'S',

			/// <summary>
			/// Несколько ответов
			/// </summary>
			Multichoice = 'M',

			/// <summary>
			/// Несколько ответов с приоритетом
			/// </summary>
			Priority = 'P',

			/// <summary>
			/// Соответствие
			/// </summary>
			Conformity = 'C'
		}
	*/

	create table Question (
		QuestId int identity(1,1),
		GroupQuestId int,
		Title varchar(500) COLLATE Cyrillic_General_CI_AS not null,
		Literal varchar(10),
		LevelOfDifficulty char(1) not null default('S'),
		AnswerType char(1) not null default('S'),
		
		CONSTRAINT [PK_Question] PRIMARY KEY (
			QuestId ASC
		)
	);
end
GO

/*
	Файловые вложения
*/
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='FileAttachment'
	) begin
	
	create table FileAttachment (
		AttachmentId int identity(1,1),
		Description varchar(4000) COLLATE Cyrillic_General_CI_AS,
		AFileName varchar(256) null,
		AFile varbinary(max) /*filestream*/ null,
		
		CONSTRAINT [PK_FileAttachment] PRIMARY KEY (
			AttachmentId ASC
		)
	);
end
GO

-- файловые приложения для вопросов
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='QuestAttachment'
	) begin

	create table QuestAttachment (
		QuestId int not null,
		AttachmentId int not null,
		Description varchar(4000) COLLATE Cyrillic_General_CI_AS,
		OrderNo int,
		
		CONSTRAINT [PK_Quest_Attachment] PRIMARY KEY (
			QuestId ASC,
			AttachmentId ASC
		),
		
		CONSTRAINT [FK1_Question]
			FOREIGN KEY (QuestId)
			REFERENCES Question(QuestId),
		
		CONSTRAINT [FK2_FileAttachment]
			FOREIGN KEY (AttachmentId)
			REFERENCES FileAttachment(AttachmentId)
	);
end
GO

/*
	Формирование тестов
*/
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='TestQuestion'
	) begin
	
	create table TestQuestion (
		TestId int not null,
		QuestId int not null,
		QuestNum int not null,
		MaxTime int,

		CONSTRAINT [PK_TestQuestion] PRIMARY KEY (
			TestId ASC,
			QuestId ASC
		),

		CONSTRAINT FK1_TestQuestion
			FOREIGN KEY (TestId)
			REFERENCES Test(TestId),

		CONSTRAINT FK2_TestQuestion
			FOREIGN KEY (QuestId)
			REFERENCES Question(QuestId)
	);
end
GO

-- ответы
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='Answer'
	) begin
	
	create table Answer (
		AnswerId int identity(1,1),
		Title varchar(1000) COLLATE Cyrillic_General_CI_AS not null,
		Literal varchar(10),
		
		CONSTRAINT [PK_Answer] PRIMARY KEY (
			AnswerId ASC
		)
	);
end
GO

-- ответы на вопрос
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='QuestAnswer'
	) begin
	
	create table QuestAnswer (
		QuestAnswerId int identity(1, 1),
		QuestId int not null,
		AnswerId int not null,
		IsRight bit not null default(0),
		PriorityNo int,
		GroupNum int,
		Description varchar(4000) COLLATE Cyrillic_General_CI_AS,

		CONSTRAINT [PK_Quest_Answer] PRIMARY KEY (
			QuestAnswerId ASC
		),
		
		CONSTRAINT [FK1_QuestAnswer_Question]
			FOREIGN KEY (QuestId)
			REFERENCES Question(QuestId),
			
		CONSTRAINT [FK2_Answer]
			FOREIGN KEY (AnswerId)
			REFERENCES Answer(AnswerId)
	)
end
GO

-- приложения к ответам в пределах вопроса
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='QuestAnswerAttachment'
	) begin
	
	create table QuestAnswerAttachment (
		QuestAnswerId int not null,
		AttachmentId int not null,
		Description varchar(4000) COLLATE Cyrillic_General_CI_AS,
		OrderNo int,

		CONSTRAINT [PK_Quest_Answer_Attachment] PRIMARY KEY (
			QuestAnswerId ASC,
			AttachmentId ASC
		),
		
		CONSTRAINT [FK1_Quest_Answer]
			FOREIGN KEY (QuestAnswerId)
			REFERENCES QuestAnswer(QuestAnswerId),
		
		CONSTRAINT [FK2_Attachment]
			FOREIGN KEY (AttachmentId)
			REFERENCES FileAttachment(AttachmentId)
	)
end
GO

-- пользователи
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='Person'
	) begin
	
	create table [Person] (
		PersonId int identity(1,1),
		Nickname varchar(200) not null,
		[Password] varchar(200) not null,
		RoleType int not null default(2), -- 1 - админ, 2 - обычный пользователь
		
		CONSTRAINT [PK_Person] PRIMARY KEY (
			PersonId ASC
		),
		
		CONSTRAINT UQC_Person_Nickname
			UNIQUE (Nickname)
			WITH (IGNORE_DUP_KEY=OFF)
	);
end
GO

-- ответы пользователя
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='PersonAnswer'
	) begin
	
	create table PersonAnswer (
		PersonId int not null,
		TestId int not null,
		QuestId int not null,
		AnswerId int not null,
		PriorityNo int,
		AnswerTime int

		CONSTRAINT [PK_PersonAnswer] PRIMARY KEY (
			PersonId ASC,
			TestId ASC,
			QuestId ASC,
			AnswerId ASC
		),

		CONSTRAINT FK1_Person
			FOREIGN KEY (PersonId)
			REFERENCES Person(PersonId),

		CONSTRAINT FK2_Test
			FOREIGN KEY (TestId)
			REFERENCES Test(TestId),
			
		CONSTRAINT FK3_Question
			FOREIGN KEY (QuestId)
			REFERENCES Question(QuestId),

		CONSTRAINT FK4_Answer
			FOREIGN KEY (AnswerId)
			REFERENCES Answer(AnswerId)
	);
end
GO

-- тестовый пользователь
-- password: 123456
insert into [Person] (NICKNAME, [PASSWORD]) values ('_testPerson_', 'e10adc3949ba59abbe56e057f20f883e')

/* очистка
	drop TABLE PersonAnswer;
	drop table Person;
	drop table TestQuestion;
	drop table QuestAnswerAttachment;
	drop table QuestAnswer;
	drop table QuestAttachment;
	drop table Question;
	drop table Answer;
	drop table Test;
	drop table FileAttachment;
*/