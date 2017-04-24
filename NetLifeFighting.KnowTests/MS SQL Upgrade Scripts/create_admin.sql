SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

declare
	@adminName varchar(200) = 'admin',
	@pass varchar(200) = 'e00cf25ad42683b3df678c61f42c6bda';--admin1

insert into Person (Nickname, [Password], RoleType) values (@adminName, @pass, 1);
GO