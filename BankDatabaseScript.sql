set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on 
go

create database [Bank_Database]
go

use [Bank_Database]
go

--Таблица "Статус заявки"

create table [dbo].[Application_Status]
(
	[ID_Application_Status] [int] not null identity (1,1),
	[Name_Status] [varchar] (50) not null,
			constraint [PK_Application_Status] primary key clustered
	([ID_Application_Status] ASC) on [PRIMARY],
	constraint [UQ_Name_Status] unique ([Name_Status])
) 
go

insert into [dbo].[Application_Status] ([Name_Status]) values
('Оформлена'),
('Зарегистрирована'),
('На рассмотрении'),
('Отказано')
go

--Таблица "Роли"

create table [dbo].[Role]
(
	[ID_Role] [int] not null identity(1,1),
	[Name_Role] varchar (50) not null,
	[Deleted] [bit] not null default('1'),
			constraint [PK_Role] primary key clustered
	([ID_Role] ASC) on [PRIMARY],
	constraint [UQ_Name_Role] unique ([Name_Role])
)
go

insert into [dbo].[Role] ([Name_Role]) values
('Администратор'),
('Пользователь')
go

--Таблица "Вид счёта"

create table [dbo].[Account_Type]
(
	[ID_Account_Type] [int] not null identity (1,1),
	[Name_Account_Type] [varchar] (50) not null,
	[Deleted] [bit] not null default('1'),
			constraint [PK_Account_Type] primary key clustered
	([ID_Account_Type] ASC) on [PRIMARY],
	constraint [UQ_Account_Type] unique ([Name_Account_Type])
) 
go

insert into [dbo].[Account_Type] ([Name_Account_Type]) values
('Текущий'),
('Расчётный'),
('Кредитный'),
('Депозитный'),
('Накопительный'),
('Бюджетный')
go

--Таблица "Вид транзакции"

create table [dbo].[Transaction_Type]
(
	[ID_Transaction_Type] [int] not null identity (1,1),
	[Name_Transaction_Type] [varchar] (50) not null,
	[Deleted] [bit] not null default('1'),
			constraint [PK_Transaction_Type] primary key clustered
	([ID_Transaction_Type] ASC) on [PRIMARY],
	constraint [UQ_Transaction_Type] unique ([Name_Transaction_Type])
) 
go

insert into [dbo].[Transaction_Type] ([Name_Transaction_Type]) values
('Перевод'),
('Списание'),
('Зачисление'),
('Наличные')
go

--Таблица "Пользователи"

create table [dbo].[User]
(
	[ID_User] [int] not null identity(1,1),
	[First_Name] [varchar] (30) not null,
	[Second_Name] [varchar] (30) not null,
	[Middle_Name] [varchar] (30) null default ('-'),
	[Birthday] [date] not null,
	[Passport_Series] [varchar] (5) not null,
	[Passport_Number] [varchar] (6) not null,
	[Login] [varchar] (32) not null,
	[Password] [varchar] (max) not null,
	[Salt] [varchar] (100) not null,
	[Role_ID] [int] not null default ('1'),
	[Deleted] [bit] not null default ('1'),
	constraint [PK_User] primary key clustered
		([ID_User] ASC) on [PRIMARY],
	constraint [FK_Role_User] foreign key ([Role_ID])
		references [dbo].[Role] ([ID_Role])
)
go

--Таблица "Кредитная заявка"

create table [dbo].[Credit_Application]
(
	[ID_Credit_Application] [int] not null identity (1,1),
	[Application_Amount] [decimal] (38,2) not null,
	[User_ID] [int] not null,
	[Status_ID] [int] not null, 
			constraint [PK_Credit_Application] primary key clustered
	([ID_Credit_Application] ASC) on [PRIMARY],
	constraint [FK_Credit_Application_User] foreign key ([User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Credit_Application_Status] foreign key ([Status_ID])
		references [dbo].[Application_Status] ([ID_Application_Status])
) 
go

--Таблица "Кредитные договора"

create table [dbo].[Credit_Agreement]
(
	[ID_Credit_Agreement] [int] not null identity (1,1),
	[Credit_Amount] [decimal] (38,2) not null,
	[Date_Drawing] [date] not null,
	[Date_Termination] [date] not null,
	[Credit_Rate] [decimal] (38,2) not null,
	[User_ID] [int] not null,
	[Credit_Application_ID] [int] not null, 
	[Deleted] [bit] not null default ('1'),
			constraint [PK_Credit_Agreement] primary key clustered
	([ID_Credit_Agreement] ASC) on [PRIMARY],
	constraint [FK_Credit_Agreement_User] foreign key ([User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Credit_Application] foreign key ([Credit_Application_ID])
		references [dbo].[Credit_Application] ([ID_Credit_Application])
) 
go

--Таблица "Заявка на открытие счёта"

create table [dbo].[Account_Application]
(
	[ID_Account_Application] [int] not null identity (1,1),
	[Type_Account_ID] [int] not null,
	[User_ID] [int] not null,
	[Status_ID] [int] not null, 
			constraint [PK_Account_Application] primary key clustered
	([ID_Account_Application] ASC) on [PRIMARY],
	constraint [FK_Account_Application_Type] foreign key ([Type_Account_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_Application_User] foreign key ([User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Account_Application_Status] foreign key ([Status_ID])
		references [dbo].[Application_Status] ([ID_Application_Status])
) 
go

--Таблица "Счёт"

create table [dbo].[Account]
(
	[ID_Account] [int] not null identity (1,1),
	[Balance] [decimal] (38,2) not null,
	[Percent] [int] not null,
	[Type_ID] [int] not null,
	[User_Account_ID] [int] not null,
	[Deleted] [bit] not null default ('1'),
			constraint [PK_Account] primary key clustered
	([ID_Account] ASC) on [PRIMARY],
	constraint [FK_Account_Type] foreign key ([Type_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_User] foreign key ([User_Account_ID])
		references [dbo].[User] ([ID_User])
) 
go

--Таблица "Банковская карта"

create table [dbo].[Card]
(
	[Card_Number] [varchar] (19) not null unique,
	[Card_Holder] [varchar] (100) not null,
	[Validity] [varchar] (5) not null,
	[Account_ID] [int] not null,
	[Deleted] [bit] not null default ('1'),
			constraint [PK_Card_Number] primary key clustered
	([Card_Number] ASC) on [PRIMARY],
	constraint [FK_Account_Card] foreign key ([Account_ID])
		references [dbo].[Account] ([ID_Account])
) 
go

--Таблица "Транзакции"

create table [dbo].[Transaction]
(
	[ID_Transaction] [int] not null identity (1,1),
	[Name_Transaction] [varchar] (max) not null,
	[Date_Transaction] [datetime] not null,
	[Summ_Transaction] [decimal] (38,2) not null,
	[Account_ID] [int] not null,
	[Transaction_Type_ID] [int] not null,
	[Deleted] [bit] not null default ('1'),
			constraint [PK_Transaction] primary key clustered
	([ID_Transaction] ASC) on [PRIMARY],
	constraint [FK_Account_Transaction_Type] foreign key ([Transaction_Type_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_Transaction] foreign key ([Account_ID])
		references [dbo].[Account] ([ID_Account])
) 
go