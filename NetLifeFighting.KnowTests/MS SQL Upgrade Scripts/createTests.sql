SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*USE dev_lf_tests;
GO*/

-- тестовые данные --

-- открыть транзакцию
begin transaction

declare @isRight bit = 1;
declare @questId int;
declare @testUserId int = 1;

-- todo: в дальнейшем использовать SCOPE_IDENTITY()
-- тесты
insert into Test (TITLE) values ('ПСС.БИЛЕТ № 1');

-- вопросы и ответы
-- вопрос 1
insert into Question (TITLE, ANSWERTYPE) values ('На Ваших глазах на ночной обезлюдевшей улице происходит нападение на одинокого прохожего. Нападающий размахивал ножом и требовал у прохожего денег, но когда тот отказал – ударил его ножом в живот. Человек с криком согнулся и упал. Нападающий обшарил его одежду, схватил кошелёк и бросился бежать, оставив нож в теле пострадавшего. Ваши действия?', 'S');
set @questId = SCOPE_IDENTITY();

insert into Answer (TITLE, LITERAL) values ('помочь раненому лечь на бок и прижать согнутые ноги к корпусу', 'A');
insert into Answer (TITLE, LITERAL) values ('помочь раненому лечь на спину и повернуть голову набок', 'B');
insert into Answer (TITLE, LITERAL) values ('позвонить в службу спасения и начать преследование бандита', 'C');
insert into Answer (TITLE, LITERAL) values ('вынуть нож и к ране плотно прижать чистый платок', 'D');
insert into Answer (TITLE, LITERAL) values ('вытащить свой смартфон и сфотографировать убегающего', 'E');
insert into Answer (TITLE, LITERAL) values ('вызывать пострадавшему скорую помощь', 'F');
insert into Answer (TITLE, LITERAL) values ('зафиксировать нож в ране с помощью подручных средств', 'G');
insert into Answer (TITLE, LITERAL) values ('дать пострадавшему стакан воды и анальгин для обезболивания', 'H');
insert into Answer (TITLE, LITERAL) values ('дать пострадавшему обильное питьё, чтобы восполнить кровопотерю', 'I');
insert into Answer (TITLE, LITERAL) values ('начать искать и опрашивать свидетелей происшествия', 'J');
insert into Answer (TITLE, LITERAL) values ('оказывать пострадавшему психологическую поддержку до приезда помощи', 'K');
insert into Answer (TITLE, LITERAL) values ('не давать пострадавшему прикасаться к ножу', 'L');
insert into Answer (TITLE, LITERAL) values ('подбежать к пострадавшему и спросить - нужна ли ему помощь?', 'M');
insert into Answer (TITLE, LITERAL) values ('проверить у пострадавшего состояние сознания', 'N');
insert into Answer (TITLE, LITERAL) values ('обеспечить пострадавшему обильный приток воздуха', 'O');

insert into QuestAnswer (QuestId, AnswerId)
	select @questId, AnswerId
	from Answer
	where AnswerId <= 15

-- вопрос 2
insert into Question (TITLE, ANSWERTYPE) values ('На Ваших глазах произошло ДТП – машина, вылетев на поворот на высокой скорости, въехала в дорожное заграждение. Капот и нос машины смят. Мотор заглох. Фары машины погасли. Вы видите в стекло водительской двери, что водитель машины остался лежать на руле неподвижно, на его голове кровь. Ваши действия?', 'S');
set @questId = SCOPE_IDENTITY();

insert into Answer (TITLE, LITERAL) values ('достать из багажника машины знак аварийной остановки и аптечку', 'A');
insert into Answer (TITLE, LITERAL) values ('позвонить в спасательную службу', 'B');
insert into Answer (TITLE, LITERAL) values ('вытащить водителя из машины для оказания помощи', 'C');
insert into Answer (TITLE, LITERAL) values ('проверить пульс на шее водителя', 'D');
insert into Answer (TITLE, LITERAL) values ('вызывать скорую помощь', 'E');
insert into Answer (TITLE, LITERAL) values ('попытаться открыть дверь водителя, если не открывается – выбить стекло', 'F');
insert into Answer (TITLE, LITERAL) values ('выбежать на дорогу, размахивая руками, чтобы привлечь внимание проезжающих машин', 'G');
insert into Answer (TITLE, LITERAL) values ('поставить знак аварийной остановки на дороге перед ДТП', 'H');
insert into Answer (TITLE, LITERAL) values ('не передвигать водителя, если этого не потребуют особые обстоятельства', 'I');
insert into Answer (TITLE, LITERAL) values ('позвонить в ГИБДД', 'J');
insert into Answer (TITLE, LITERAL) values ('перевязать рану пострадавшему', 'K');
insert into Answer (TITLE, LITERAL) values ('прижать к ране салфетку или бинт', 'L');
insert into Answer (TITLE, LITERAL) values ('отключить зажигание в машине', 'M');
insert into Answer (TITLE, LITERAL) values ('при отсутствии у водителя пульса и дыхания - вытащить из машины', 'N');
insert into Answer (TITLE, LITERAL) values ('следить за состоянием пульса и дыхания водителя', 'O');

insert into QuestAnswer (QuestId, AnswerId)
	select @questId, AnswerId
	from Answer
	where AnswerId between 16 and 30

-- вопрос 3
insert into Question (TITLE, ANSWERTYPE) values ('Беременная женщина на сроке до 5 месяцев, "похватила простуду". К вечеру у неё значительно поднялась температура. Смерили - оказалось 37.9. Вы вызвали "скорую помощь", но известно, что медики прибудут только через несколько часов. Женщина страдает от повышения температуры и боязни за сохранность плода. Ваши действия?', 'S');
set @questId = SCOPE_IDENTITY();

insert into Answer (TITLE, LITERAL) values ('Уложить женщину в постель, укутать одеялом, чтобы пропотела', 'A');
insert into Answer (TITLE, LITERAL) values ('Уложить женщину в постель и положить на лоб холодный компресс', 'B');
insert into Answer (TITLE, LITERAL) values ('Уложить женщину в постель, на ноги сделать тёплую грелку', 'C');
insert into Answer (TITLE, LITERAL) values ('Если температура поднимается - дать женщине выпить таблетку аспирина', 'D');
insert into Answer (TITLE, LITERAL) values ('Если температура поднимается - дать выпить таблетку ибупрофена', 'E');
insert into Answer (TITLE, LITERAL) values ('Если температура поднимается - дать выпить полтаблетки парацетамола', 'F');
insert into Answer (TITLE, LITERAL) values ('Дать выпить порошок противопростудный - Фервекс, Колдрекс и т.д.', 'G');
insert into Answer (TITLE, LITERAL) values ('Заварить и дать выпить чай с ромашкой, женьшенем и шалфеем', 'H');
insert into Answer (TITLE, LITERAL) values ('Заварить и дать выпить чай чёрный на малиновом варенье', 'I');
insert into Answer (TITLE, LITERAL) values ('Заварить и дать выпить крепкий чай зелёный с лимоном', 'J');
insert into Answer (TITLE, LITERAL) values ('Протереть тело пострадавшей мокрым прохладным полотенцем', 'K');
insert into Answer (TITLE, LITERAL) values ('Протереть виски, подмышки и живот пострадавшей спиртом', 'L');
insert into Answer (TITLE, LITERAL) values ('Помощь женщине принять прохладный душ', 'M');
insert into Answer (TITLE, LITERAL) values ('Вызывать платного гениколога на дом', 'N');
insert into Answer (TITLE, LITERAL) values ('Помочь женщине одеться и на такси везти её в роддом', 'O');

insert into QuestAnswer (QuestId, AnswerId)
	select @questId, AnswerId
	from Answer
	where AnswerId between 31 and 45

-- вопрос 4
insert into Question (TITLE, ANSWERTYPE) values ('"Вы пришли в гости к другу. Пока вы общались, его престарелой матери позвонили и сообщили дурную весть. Пожилая женщина сильно расстроилась, а после того, как положила трубку, схватилась за грудь и застонала. Она чувствует сильную боль в груди слева, боль в левой руке и левой части головы. Друг принёс атечку. В ней - аспирин, аскорбиновая кислота, корвалол, нитроглицирин, валерьянка, валидол и пустырник. Ваши действия?"', 'S');
set @questId = SCOPE_IDENTITY();

insert into Answer (TITLE, LITERAL) values ('вызывать скорую помощь', 'A');
insert into Answer (TITLE, LITERAL) values ('дать женщине выпить стакан воды, чтобы успокоилась и дать ей выговориться', 'B');
insert into Answer (TITLE, LITERAL) values ('уложить женщину на постель', 'C');
insert into Answer (TITLE, LITERAL) values ('посадить в кресло или на кровать, колени помочь прижать к груди', 'D');
insert into Answer (TITLE, LITERAL) values ('положить так, чтобы ноги были выше головы', 'E');
insert into Answer (TITLE, LITERAL) values ('узнать, не было ли такого раньше и, если было, то какое лекарство тогда принимали', 'F');
insert into Answer (TITLE, LITERAL) values ('сразу дать ей рассосать валидол', 'G');
insert into Answer (TITLE, LITERAL) values ('дать ей рассосать нитроглицирин', 'H');
insert into Answer (TITLE, LITERAL) values ('срочно дать ей выпить валерьянку', 'I');
insert into Answer (TITLE, LITERAL) values ('дать выпить таблетку аспирина с полстаканом воды', 'J');
insert into Answer (TITLE, LITERAL) values ('узнать, не принимает ли его мать каких-либо лекарств в таких ситуациях', 'K');
insert into Answer (TITLE, LITERAL) values ('дать 15 капель корвалола в воде и одну таблетку пустырника', 'L');
insert into Answer (TITLE, LITERAL) values ('следить за состоянием пульса и давления', 'M');
insert into Answer (TITLE, LITERAL) values ('обеспечить приток воздуха для полноценного дыхания', 'N');
insert into Answer (TITLE, LITERAL) values ('спросить у друга разрешения помочь его матери', 'O');

insert into QuestAnswer (QuestId, AnswerId)
	select @questId, AnswerId
	from Answer
	where AnswerId between 46 and 60

-- вопрос 5
insert into Question (TITLE, ANSWERTYPE) values ('На летнем пикнике возле реки вдали от города Вы наблюдаете, как человек внезапно кричит и хватается за ногу. В траву от него уползает светлая узорчатая змея. На ноге явственно проявляются кровавые пятна. Человек в панике, носится и пытается найти свой телефон, чтобы позвонить в скорую помощь. Ваши действия?', 'S');
set @questId = SCOPE_IDENTITY();

insert into Answer (TITLE, LITERAL) values ('дать человеку обильное питьё', 'A');
insert into Answer (TITLE, LITERAL) values ('сдавить пострадавшую ногу тугим бинтованием от стопы до паха', 'B');
insert into Answer (TITLE, LITERAL) values ('высосать яд из раны, каждый раз сплёвывая и прополаскивая рот чистой водой', 'C');
insert into Answer (TITLE, LITERAL) values ('позвонить в скорую помощь', 'D');
insert into Answer (TITLE, LITERAL) values ('с помощью чистого ножа увеличить ранки и выдавить яд с кровью', 'E');
insert into Answer (TITLE, LITERAL) values ('прижечь рану с помощью зажигалки и накалённого края металлической ложки', 'F');
insert into Answer (TITLE, LITERAL) values ('наложить свободную стерильную повязку на рану', 'G');
insert into Answer (TITLE, LITERAL) values ('наложить давящую повязку на рану', 'H');
insert into Answer (TITLE, LITERAL) values ('обработать края раны йодом или спиртом', 'I');
insert into Answer (TITLE, LITERAL) values ('не давать человеку останавливаться, чтобы движение изгнало яд из мышцы', 'J');
insert into Answer (TITLE, LITERAL) values ('на место укуса «поставить банку», как при простуде', 'K');
insert into Answer (TITLE, LITERAL) values ('срочно заставить человека лечь и не давать двигаться', 'L');
insert into Answer (TITLE, LITERAL) values ('найти укусившую змею, чтобы врачи могли определить состав её яда', 'M');
insert into Answer (TITLE, LITERAL) values ('ждать приезда скорой помощи', 'N');
insert into Answer (TITLE, LITERAL) values ('соорудить носилки и нести человека к ближайшему населённому пункту', 'O');

insert into QuestAnswer (QuestId, AnswerId)
	select @questId, AnswerId
	from Answer
	where AnswerId between 61 and 75

-- привязка вопросов к тесту
insert into TestQuestion (TestId, QuestId, QuestNum)
	select 1, QuestId, QuestId from Question


insert into Test (TITLE) values ('ПСС.БИЛЕТ № 2');
insert into Test (TITLE) values ('ПСС.БИЛЕТ № 3');
insert into Test (TITLE) values ('ПСС.БИЛЕТ № 4');
insert into Test (TITLE) values ('ПСС.БИЛЕТ № 5');

-- в случае успешной вставки транзакцию закрыть
if @@ERROR = 0 begin
	commit transaction;
end
else begin
	rollback transaction;
end