use [Bank_Database]
go

create or alter view [dbo].[User_Credit_Account_Info] ( "���", "�������", "����� ����. ����������", "����� �������", "���� ����������",
	"���� ����������", "���������� ������", "����� ������ �� ������", "����� �����", "������", "�������", "��� �����", "����� �����", "��������� �����", "���� �������� �����") as

	select
		[First_Name] + ' ' + [Second_Name]  + ' ' +  [Middle_Name] as "���",
		[Passport_Series] + ' ' + [Passport_Number] as "�������",
		[ID_Credit_Agreement] as "����� ����. ��������",
		[Application_Amount] as "����� �������",
		[Date_Drawing] as "���� ����������",
		[Date_Termination] as "���� ����������",
		[Credit_Rate] as "���������� ������",
		[ID_Credit_Application] as "����� ������ �� ������",
		[CardAccount_ID] as "����� �����",
		[Balance] as "������",
		[Percent] as "�������",
		[Name_Account_Type]  as "��� �����",
		[Card_Number] as "����� �����",
		[Card_Holder] as "��������� �����",
		[Validity] as "���� ��������" from [dbo].[User]

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

create or alter view [dbo].[Account_Info] ("����� �����", "�������", "��� �����", "����� �����", "��������� �����",
		"���� ��������", "����� ������ �� �������� �����", "������ ������") as

	select 
		[ID_Account] as "����� �����",
		[Percent] as "�������",
		[Name_Account_Type]  as "��� �����",
		[Card_Number] as "����� �����",
		[Card_Holder] as "��������� �����",
		[Validity] as "���� ��������",
		[ID_Account_Application] as "����� ������ �� �������� �����",
		[Name_Status] as "������ ������" from [dbo].[Account_Application]

		inner join [dbo].[Application_Status] on [Status_ID] = [ID_Application_Status]
		inner join [dbo].[Account] on [User_Account_ID] = [User_Account_ID]
		inner join [dbo].[Card] on [ID_Account] = [CardAccount_ID]
		inner join [dbo].[Account_Type] on [Type_ID] = [ID_Account_Type]

	where
		[Name_Status] = '���������'
go

create or alter view [dbo].[Transactions_Overview] ("�������� ����������", "���� ����������", "����� ����������", "����� �����", "��� ����������",
		"����� �����", "��������� �����", "���� �������� �����", "��� �����") as

select
    [Name_Transaction] as "�������� ����������",
    [Date_Transaction] as "���� ����������",
    [Summ_Transaction] as "����� ����������",
    CONVERT(bigint, [ID_Account]) as "����� �����",
    [Name_Transaction_Type] as "��� ����������",
    [Card_Number] as "����� �����",
    [Card_Holder] as "��������� �����",
    [Validity] as "���� �������� �����",	
    [Name_Account_Type] as "��� �����" from [dbo].[Transaction]

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

create or alter view [dbo].[CreditApplication_Info]  ("����� ������", "����� ������", "�������� ���������� ������", "����������� ���������� ������", "����� ��������", "���� ������", "���� ���������") as

select
    [ID_Credit_Application] as "����� ������",
    [Application_Amount] as "����� ������",
	[Credit_Desired_Percentage] as "�������� ���������� ������",
    [Credit_Rate] as "����������� ���������� ������",
    [ID_Credit_Agreement] as "����� ��������",
    [Date_Drawing] as "���� ������",
    [Date_Termination] as "���� ���������" from [dbo].[Credit_Application]

    inner join [dbo].[Application_Status] on [Status_ID] = [ID_Application_Status]
    inner join [dbo].[Credit_Agreement] on [ID_Credit_Application] = [Credit_Application_ID]

where
    [Name_Status] = '���������'
go

select * from [dbo].[User_Credit_Account_Info]
go

select * from [dbo].[Account_Info]
go

select * from [dbo].[Transactions_Overview]
go

select * from [dbo].[CreditApplication_Info]
go
