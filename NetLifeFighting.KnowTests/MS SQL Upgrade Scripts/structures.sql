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
		TestId int identity(1,1) primary key,
		Title varchar(150) COLLATE Cyrillic_General_CI_AS not null,
		MaxTime int,
		LevelOfDifficulty char(1) not null default('S'),
		RelevanceStatus char(1) not null default('R'),
		RelevanceDate datetime
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
	ANSWERTYPE:
		R - соответствие,
		M - несколько вариантов ответа,
		S - последовательность
	*/

	create table Question (
		QuestId int identity(1,1) primary key,
		GroupQuestId int,
		Title varchar(500) COLLATE Cyrillic_General_CI_AS not null,
		Literal varchar(10),
		LevelOfDifficulty char(1) not null default('S'),
		AnswerType char(1) not null default('S')
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
		TestId int not null references Test(TestId),
		QuestId int not null references Question(QuestId),
		QuestNum int not null,
		MaxTime int,

		CONSTRAINT [PK_TestQuestion] PRIMARY KEY (
			TestId ASC,
			QuestId ASC
		)
		
		/*,

		CONSTRAINT FK1_TestQuestion
			FOREIGN KEY (TestId)
			REFERENCES Test(TestId)
			ON DELETE CASCADE,

		CONSTRAINT FK2_TestQuestion
			FOREIGN KEY (QuestId)
			REFERENCES Question(QuestId)
			ON DELETE CASCADE
		*/
	);
end
GO

-- ответы
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='Answer'
	) begin
	
	/*
		Для соответствий в качестве заголовка вставлять формулировки пары ответов через дефис,
		хранить все возможные связи для соответствий.
	*/
	create table Answer (
		AnswerId int identity(1,1) primary key,
		Title varchar(200) COLLATE Cyrillic_General_CI_AS not null,
		Literal varchar(10)
	);
end
GO

-- ответы
if not exists (	
			select 1 
			from INFORMATION_SCHEMA.TABLES 
			where TABLE_TYPE='BASE TABLE' and TABLE_NAME='QuestAnswer'
	) begin
	
	create table QuestAnswer (
		QuestId int not null references Question(QuestId),
		AnswerId int not null references  Answer( AnswerId),
		IsRight bit not null default(0),
		PriorityNo int,
		GroupNum int,

		CONSTRAINT [PK_Quest_Answer] PRIMARY KEY (
			QuestId ASC,
			AnswerId ASC
		)
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
		PersonId int identity(1,1) primary key,
		Nickname varchar(200) not null,
		[Password] varchar(200) not null,
		RoleType int not null default(2) -- 1 - админ, 2 - обычный пользователь
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
		PersonId int not null references Person(PersonId),
		TestId int not null references Test(TestId),
		QuestId int not null references Question(QuestId),
		AnswerId int not null references Answer(AnswerId),
		PriorityNo int,
		AnswerTime int

		CONSTRAINT [PK_PersonAnswer] PRIMARY KEY (
			PersonId ASC,
			TestId ASC,
			QuestId ASC,
			AnswerId ASC
		)
		
		/*
		,

		CONSTRAINT FK1_PersonAnswer
			FOREIGN KEY (PersonId)
			REFERENCES [Person](PersonId)
			ON DELETE CASCADE,

		CONSTRAINT FK2_PersonAnswer
			FOREIGN KEY (AnswerId)
			REFERENCES Answer(AnswerId)
			ON DELETE CASCADE
		*/
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
		AttachmentId int identity(1,1) not null,
		Description varchar(4000) null,
		AFileName varchar(256) null,
		QuestId int not null references Question(QuestId),
		AFile varbinary(max) filestream null
	);
end
GO

-- тестовый пользователь
-- password: 123456
insert into [Person] (NICKNAME, [PASSWORD]) values ('_testPerson_', 'e10adc3949ba59abbe56e057f20f883e')

/* очистка
DROP TABLE PersonAnswer;
drop table Person;
drop table TestQuestion;
drop table QuestAnswer;
drop table QUESTION;
drop table Answer;
drop table Test;
*/