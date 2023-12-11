use [Bank_Database]
go

create table [dbo].[Credit_Agreement_History]
(
	[ID_Credit_Agreement_History] [int] not null identity (1,1) primary key,
	[Credit_Agreement_ID] [varchar] (20) not null,
	[Date_Time] [datetime] not null default (getdate()),
	[Change_Type] [varchar] (50) not null,
	[Changed_Column] [varchar] (50) not null,
	[Old_Value] [varchar] (max) not null default ('-'),
	[New_Value] [varchar] (max) not null default ('-')
)
go

create or alter trigger [dbo].[Credit_Agreement_History_Trigger] on [dbo].[Credit_Agreement]
after insert, update, delete
as
begin

    if exists (select * from inserted)
    begin
        if exists (select * from deleted)
        begin
            -- Обновленные записи
            insert into [dbo].[Credit_Agreement_History] ([Credit_Agreement_ID], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                i.ID_Credit_Agreement,
                'Обновление',
                'Date_Drawing',
                cast(d.Date_Drawing as varchar(max)),
                cast(i.Date_Drawing as varchar(max))
            from
                inserted i
            inner join deleted d on i.ID_Credit_Agreement = d.ID_Credit_Agreement
            where
                isnull(i.Date_Drawing, '') <> isnull(d.Date_Drawing, '')
        end
        else
        begin
            -- Новые записи
            insert into [dbo].[Credit_Agreement_History] ([Credit_Agreement_ID], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                ID_Credit_Agreement,
                'Добавление',
                'Date_Drawing',
                '-',
                cast(Date_Drawing as varchar(max))
            from
                inserted
        end
    end
    else if exists (select * from deleted)
    begin
        -- Удаленные записи
        insert into [dbo].[Credit_Agreement_History] ([Credit_Agreement_ID], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
        select
            ID_Credit_Agreement,
            'Удаление',
            'Date_Drawing',
            cast(Date_Drawing as varchar(max)),
            '-'
        from
            deleted
    end
end

create table [dbo].[Account_Application_History]
(
    [ID_Account_Application_History] [int] not null identity (1,1) primary key,
    [Account_Application_ID] [varchar] (20) NOT NULL,
    [Date_Time] [datetime] NOT NULL,
    [Change_Type] [varchar] (50) NOT NULL,
    [Changed_Column] [varchar] (50) NOT NULL,
    [Old_Value] [varchar] (max) not null default ('-'),
    [New_Value] [varchar] (max) not null default ('-')
)
go

create or alter trigger [dbo].[Account_Application_History_Trigger] on [dbo].[Account_Application]
after insert, update, delete
as
begin

    if exists (select * from inserted)
    begin
        if exists (select * from deleted)
        begin
            -- Обновленные записи
            insert into [dbo].[Account_Application_History] ([Account_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                i.ID_Account_Application,
                GETDATE(),
                'Обновление',
                'Account_Desired_Percentage',
                ISNULL(cast(d.Account_Desired_Percentage as varchar(max)), 'NULL'),
                ISNULL(cast(i.Account_Desired_Percentage as varchar(max)), 'NULL')
            from
                inserted i
            inner join deleted d on i.ID_Account_Application = d.ID_Account_Application
            where
                isnull(i.Account_Desired_Percentage, '') <> isnull(d.Account_Desired_Percentage, '')
        end
        else
        begin
            -- Новые записи
            insert into [dbo].[Account_Application_History] ([Account_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                ID_Account_Application,
                GETDATE(),
                'Добавление',
                'Account_Desired_Percentage',
                '-',
                ISNULL(cast(Account_Desired_Percentage as varchar(max)), 'NULL')
            from
                inserted
        end
    end
    else if exists (select * from deleted)
    begin
        -- Удаленные записи
        insert into [dbo].[Account_Application_History] ([Account_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
        select
            ID_Account_Application,
            GETDATE(),
            'Удаление',
            'Account_Desired_Percentage',
            ISNULL(cast(Account_Desired_Percentage as varchar(max)), 'NULL'),
            '-'
        from
            deleted
    end
end

create table [dbo].[Credit_Application_History]
(
    [ID_Credit_Application_History] [int] not null identity (1,1) primary key,
    [Credit_Application_ID] [varchar](20) not null,
    [Date_Time] [datetime] not null,
    [Change_Type] [varchar](50) not null,
    [Changed_Column] [varchar](50) not null,
    [Old_Value] [varchar](max) not null default ('-'),
    [New_Value] [varchar](max) not null default ('-')
)
go

create or alter trigger [dbo].[Credit_Application_History_Trigger] on [dbo].[Credit_Application]
after insert, update, delete
as
begin
    if exists (select * from inserted)
    begin
        if exists (select * from deleted)
        begin
            -- Обновленные записи
            insert into [dbo].[Credit_Application_History] 
            ([Credit_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                i.ID_Credit_Application,
                GETDATE(),
                'Обновление',
                'Application_Amount',
                ISNULL(cast(d.Application_Amount as varchar(max)), 'NULL'),
                ISNULL(cast(i.Application_Amount as varchar(max)), 'NULL')
            from
                inserted i
            inner join deleted d on i.ID_Credit_Application = d.ID_Credit_Application
            where
                isnull(i.Application_Amount, '') <> isnull(d.Application_Amount, '')
        end
        else
        begin
            -- Новые записи
            insert into [dbo].[Credit_Application_History] 
            ([Credit_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
            select
                ID_Credit_Application,
                GETDATE(),
                'Добавление',
                'Application_Amount',
                '-',
                ISNULL(cast(Application_Amount as varchar(max)), 'NULL')
            from
                inserted
        end
    end
    else if exists (select * from deleted)
    begin
        -- Удаленные записи
        insert into [dbo].[Credit_Application_History] 
        ([Credit_Application_ID], [Date_Time], [Change_Type], [Changed_Column], [Old_Value], [New_Value])
        select
            ID_Credit_Application,
            GETDATE(),
            'Удаление',
            'Application_Amount',
            ISNULL(cast(Application_Amount as varchar(max)), 'NULL'),
            '-'
        from
            deleted
    end
end

select * from [dbo].[Credit_Application_History]
go

select * from [dbo].[Account_Application_History]
go

select * from [dbo].[Credit_Agreement_History]
go