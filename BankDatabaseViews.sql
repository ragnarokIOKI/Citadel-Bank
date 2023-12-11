use [Bank_Database]
go

create or alter view [dbo].[User_Credit_Account_Info] ( "ФИО", "Паспорт", "Номер кред. соглашения", "Сумма кредита", "Дата заключения",
	"Дата завершения", "Процентная ставка", "Номер заявки на кредит", "Номер счета", "Баланс", "Процент", "Тип счета", "Номер карты", "Держатель карты", "Срок действия карты") as

	select
		[First_Name] + ' ' + [Second_Name]  + ' ' +  [Middle_Name] as "ФИО",
		[Passport_Series] + ' ' + [Passport_Number] as "Паспорт",
		[ID_Credit_Agreement] as "Номер кред. договора",
		[Application_Amount] as "Сумма кредита",
		[Date_Drawing] as "Дата заключения",
		[Date_Termination] as "Дата завершения",
		[Credit_Rate] as "Процентная ставка",
		[ID_Credit_Application] as "Номер заявки на кредит",
		[CardAccount_ID] as "Номер счета",
		[Balance] as "Баланс",
		[Percent] as "Процент",
		[Name_Account_Type]  as "Тип счета",
		[Card_Number] as "Номер карты",
		[Card_Holder] as "Держатель карты",
		[Validity] as "Срок действия" from [dbo].[User]

		left join [dbo].[Role] on [Role_ID] = [ID_Role]
		left join [dbo].[Credit_Agreement]  on [ID_User] = [CreditAgreement_User_ID]
		left join [dbo].[Credit_Application] on [Credit_Application_ID] = [ID_Credit_Application]
		left join [dbo].[Account] on [ID_User] = [User_Account_ID]
		left join [dbo].[Card] on [ID_Account] = [CardAccount_ID]
		left join [dbo].[Account_Type] on [ID_Account_Type] = [Type_ID]

	where
		[User_Deleted] = 1
		and [CreditAgreement_Deleted] = 1
		and [Account_Deleted] = 1
		and [Card_Deleted] = 1
go

create or alter view [dbo].[Account_Info] ("Номер счета", "Процент", "Тип счета", "Номер карты", "Держатель карты",
		"Срок действия", "Номер заявки на открытие счета", "Статус заявки") as

	select 
		[ID_Account] as "Номер счета",
		[Percent] as "Процент",
		[Name_Account_Type]  as "Тип счета",
		[Card_Number] as "Номер карты",
		[Card_Holder] as "Держатель карты",
		[Validity] as "Срок действия",
		[ID_Account_Application] as "Номер заявки на открытие счета",
		[Name_Status] as "Статус заявки" from [dbo].[Account_Application]

		inner join [dbo].[Application_Status] on [Status_ID] = [ID_Application_Status]
		inner join [dbo].[Account] on [User_Account_ID] = [User_Account_ID]
		inner join [dbo].[Card] on [ID_Account] = [CardAccount_ID]
		inner join [dbo].[Account_Type] on [Type_ID] = [ID_Account_Type]

	where
		[Name_Status] = 'Оформлена'
go

create or alter view [dbo].[Transactions_Overview] ("Название транзакции", "Дата транзакции", "Сумма транзакции", "Номер счета", "Тип транзакции",
		"Номер карты", "Держатель карты", "Срок действия карты", "Тип счета") as

select
    [Name_Transaction] as "Название транзакции",
    [Date_Transaction] as "Дата транзакции",
    [Summ_Transaction] as "Сумма транзакции",
    CONVERT(bigint, [ID_Account]) as "Номер счета",
    [Name_Transaction_Type] as "Тип транзакции",
    [Card_Number] as "Номер карты",
    [Card_Holder] as "Держатель карты",
    [Validity] as "Срок действия карты",	
    [Name_Account_Type] as "Тип счета" from [dbo].[Transaction]

    inner join [dbo].[Transaction_Type] on [Transaction_Type_ID] = [ID_Transaction_Type]
    inner join [dbo].[Account] on [ID_Account] = [Transaction_Account_ID]
    inner join [dbo].[Card] on [ID_Account] = [CardAccount_ID]
    inner join [dbo].[Account_Type] on [Type_ID] = [ID_Account_Type]

where
    [Transaction_Deleted] = 1
    and [TransactionType_Deleted] = 1
    and [Account_Deleted] = 1
    and [Card_Deleted] = 1
    and [AccountType_Deleted] = 1
go

create or alter view [dbo].[CreditApplication_Info]  ("Номер заявки", "Сумма заявки", "Желаемая процентная ставка", "Фактическая процентная ставка", "Номер договора", "Дата выдачи", "Дата окончания") as

select
    [ID_Credit_Application] as "Номер заявки",
    [Application_Amount] as "Сумма заявки",
	[Credit_Desired_Percentage] as "Желаемая процентная ставка",
    [Credit_Rate] as "Фактическая процентная ставка",
    [ID_Credit_Agreement] as "Номер договора",
    [Date_Drawing] as "Дата выдачи",
    [Date_Termination] as "Дата окончания" from [dbo].[Credit_Application]

    inner join [dbo].[Application_Status] on [Status_ID] = [ID_Application_Status]
    inner join [dbo].[Credit_Agreement] on [ID_Credit_Application] = [Credit_Application_ID]

where
    [Name_Status] = 'Оформлена'
go

select * from [dbo].[User_Credit_Account_Info]
go

select * from [dbo].[Account_Info]
go

select * from [dbo].[Transactions_Overview]
go

select * from [dbo].[CreditApplication_Info]
go
