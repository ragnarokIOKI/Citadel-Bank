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

select * from account_application

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
	[Role_Deleted] [bit] not null default('1'),
			constraint [PK_Role] primary key clustered
	([ID_Role] ASC) on [PRIMARY],
	constraint [UQ_Name_Role] unique ([Name_Role])
)
go

insert into [dbo].[Role] ([Name_Role]) values
('Администратор'),
('Пользователь'),
('Кредитолог'),
('Бухгалтер по банковским операциям')
go

--Таблица "Вид счёта"

create table [dbo].[Account_Type]
(
	[ID_Account_Type] [int] not null identity (1,1),
	[Name_Account_Type] [varchar] (50) not null,
	[AccountType_Deleted] [bit] not null default('1'),
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
	[TransactionType_Deleted] [bit] not null default('1'),
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
	[Role_ID] [int] not null default ('2'),
	[User_Deleted] [bit] not null default ('1'),
	constraint [PK_User] primary key clustered
		([ID_User] ASC) on [PRIMARY],
	constraint [FK_Role_User] foreign key ([Role_ID])
		references [dbo].[Role] ([ID_Role])
)
go

insert into [dbo].[User] ([First_Name], [Second_Name], [Middle_Name], [Birthday], [Passport_Series], [Passport_Number], [Login], [Password], [Salt], [Role_ID], [User_Deleted])
values ('Брант', 'Кирилл', 'Григорьевич', '18.03.1991', '41 51', '879876', 'ivanov1979', 'R9LwT@', '%hXIPP', 1, 1)
go

--Таблица "Кредитная заявка"

create table [dbo].[Credit_Application]
(
	[ID_Credit_Application] [varchar] (20) not null unique,
	[Application_Amount] [decimal] (38,2) not null,
	[Credit_Desired_Percentage] [decimal] (38,2) not null,
	[CreditApplication_User_ID] [int] not null,
	[Status_ID] [int] not null, 
			constraint [PK_Credit_Application] primary key clustered
	([ID_Credit_Application] ASC) on [PRIMARY],
	constraint [FK_Credit_Application_User] foreign key ([CreditApplication_User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Credit_Application_Status] foreign key ([Status_ID])
		references [dbo].[Application_Status] ([ID_Application_Status])
) 
go

insert into [dbo].[Credit_Application] ([ID_Credit_Application], [Application_Amount], [Credit_Desired_Percentage], [CreditApplication_User_ID], [Status_ID]) values
('1067906672', 200000.00, 3.3, 1,1)
go

--insert into [dbo].[Credit_Application] ([ID_Credit_Application], [Application_Amount], [Credit_Desired_Percentage], [CreditApplication_User_ID], [Status_ID]) values
--('1067906670', 500000.00, 8.4, 1,1)
--go

--Таблица "Кредитные договора"

create table [dbo].[Credit_Agreement]
(
	[ID_Credit_Agreement] [varchar] (20) not null unique,
	[Date_Drawing] [date] not null,
	[Date_Termination] [date] not null,
	[Credit_Rate] [decimal] (38,2) not null,
	[CreditAgreement_User_ID] [int] not null,
	[Credit_Application_ID] [varchar] (20) not null, 
	[CreditAgreement_Deleted] [bit] not null default ('1'),
			constraint [PK_Credit_Agreement] primary key clustered
	([ID_Credit_Agreement] ASC) on [PRIMARY],
	constraint [FK_Credit_Agreement_User] foreign key ([CreditAgreement_User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Credit_Application] foreign key ([Credit_Application_ID])
		references [dbo].[Credit_Application] ([ID_Credit_Application])
) 
go

insert into [dbo].[Credit_Agreement] ([ID_Credit_Agreement], [Date_Drawing], [Date_Termination], [Credit_Rate], [CreditAgreement_User_ID], [Credit_Application_ID]) values
('57622241622', '07.09.2023', '07.09.2033', 8.4, 1, '1067906672')
go

--insert into [dbo].[Credit_Agreement] ([ID_Credit_Agreement], [Date_Drawing], [Date_Termination], [Credit_Rate], [CreditAgreement_User_ID], [Credit_Application_ID]) values
--('57622241624', '10.09.2023', '10.09.2033', 8.8, 1, '1067906673')
--go

select * from [dbo].[Credit_Agreement]
go

--Таблица "Заявка на открытие счёта"

create table [dbo].[Account_Application]
(
	[ID_Account_Application] [varchar] (20) not null unique,
	[Type_Account_ID] [int] not null,
	[Account_Desired_Percentage] [decimal] (38,2) not null,
	[AccountApplication_User_ID] [int] not null,
	[Status_ID] [int] not null, 
			constraint [PK_Account_Application] primary key clustered
	([ID_Account_Application] ASC) on [PRIMARY],
	constraint [FK_Account_Application_Type] foreign key ([Type_Account_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_Application_User] foreign key ([AccountApplication_User_ID])
		references [dbo].[User] ([ID_User]),
	constraint [FK_Account_Application_Status] foreign key ([Status_ID])
		references [dbo].[Application_Status] ([ID_Application_Status])
) 
go

ALTER TABLE [dbo].[Account_Application] ADD [Bank_Card_Needed] BIT
GO

insert into [dbo].[Account_Application] ([ID_Account_Application], [Type_Account_ID], [Account_Desired_Percentage], [AccountApplication_User_ID], [Status_ID]) values
('53335982197', 1, 10.2, 1, 1)
go

--insert into [dbo].[Account_Application] ([ID_Account_Application], [Type_Account_ID], [Account_Desired_Percentage], [AccountApplication_User_ID], [Status_ID]) values
--('53335982190', 1, 5.2, 1, 1)
--go

--Таблица "Счёт"

create table [dbo].[Account]
(
	[ID_Account] [varchar] (20) not null unique,
	[Balance] [decimal] (38,2) not null,
	[Percent] [int] not null,
	[Type_ID] [int] not null,
	[User_Account_ID] [int] not null,
	[Account_Deleted] [bit] not null default ('1'),
			constraint [PK_Account] primary key clustered
	([ID_Account] ASC) on [PRIMARY],
	constraint [FK_Account_Type] foreign key ([Type_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_User] foreign key ([User_Account_ID])
		references [dbo].[User] ([ID_User])
) 
go

ALTER TABLE [dbo].[Account] ADD [Account_Deletion_Reason] varchar(30)
GO

insert into [dbo].[Account] ([ID_Account], [Balance], [Percent], [Type_ID], [User_Account_ID]) values
('40149204400', 500.3, 3.3, 1, 1)
go

select * from [dbo].[Account]
go

--Таблица "Банковская карта"

create table [dbo].[Card]
(
	[Card_Number] [varchar] (19) not null unique,
	[Card_Holder] [varchar] (100) not null,
	[Validity] [varchar] (5) not null,
	[CCV] [varchar] (3) not null,
	[CardAccount_ID] [varchar] (20) not null,
	[Card_Deleted] [bit] not null default ('1'),
			constraint [PK_Card_Number] primary key clustered
	([Card_Number] ASC) on [PRIMARY],
	constraint [FK_Account_Card] foreign key ([CardAccount_ID])
		references [dbo].[Account] ([ID_Account])
) 
go

ALTER TABLE [dbo].[Card] ADD [Card_Deletion_Reason] varchar(30)
GO

insert into [dbo].[Card] ([Card_Number], [Card_Holder], [Validity], [CCV],  [CardAccount_ID]) values
('4414 3678 6507 9613', 'Brant Kirill',  '10/23', '779', '40149204400')
go

--Таблица "Транзакции"

create table [dbo].[Transaction]
(
	[ID_Transaction] [int] not null identity (1,1),
	[Name_Transaction] [varchar] (max) not null,
	[Date_Transaction] [datetime] not null,
	[Summ_Transaction] [decimal] (38,2) not null,
	[Transaction_Account_ID] [varchar] (20) not null,
	[Transaction_Type_ID] [int] not null,
	[Transaction_Deleted] [bit] not null default ('1'),
			constraint [PK_Transaction] primary key clustered
	([ID_Transaction] ASC) on [PRIMARY],
	constraint [FK_Account_Transaction_Type] foreign key ([Transaction_Type_ID])
		references [dbo].[Account_Type] ([ID_Account_Type]),
	constraint [FK_Account_Transaction] foreign key ([Transaction_Account_ID])
		references [dbo].[Account] ([ID_Account])
) 
go

insert into [dbo].[Transaction] ([Name_Transaction], [Date_Transaction], [Summ_Transaction], [Transaction_Account_ID], [Transaction_Type_ID]) values
('Пополнение телефона', '07.09.2023 16:38:54', -500.00, '40149204400', 2)
go
