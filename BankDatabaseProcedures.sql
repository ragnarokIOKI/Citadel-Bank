use [Bank_Database]
go

--Получение списка транзакций для определенного пользователя

CREATE OR ALTER PROCEDURE [dbo].[Get_Transactions_By_User]
	@User_ID INT
AS
BEGIN
	SELECT
		[ID_Transaction],
		[Name_Transaction],
		[Date_Transaction],
		[Summ_Transaction],
		[Transaction_Account_ID],
		[Name_Transaction_Type] FROM [dbo].[Transaction]

	INNER JOIN [dbo].[Account] ON [Transaction_Account_ID] = [ID_Account]
	INNER JOIN [dbo].[Transaction_Type] ON [Transaction_Type_ID] = [ID_Transaction_Type]

	WHERE [User_Account_ID] = @User_ID AND [Transaction_Deleted] = 1
	ORDER BY [Date_Transaction] DESC
END
GO

--Выполнение транзакции на счете

CREATE OR ALTER PROCEDURE [dbo].[Execute_Transaction]
	@Name_Transaction NVARCHAR(MAX),
	@Summ_Transaction DECIMAL(38, 2),
	@Transaction_Account_ID VARCHAR(20),
	@Transaction_Type_ID INT
AS
BEGIN
	DECLARE @Date_Transaction DATETIME = GETDATE()

	INSERT INTO [dbo].[Transaction] ([Name_Transaction], [Date_Transaction], [Summ_Transaction], [Transaction_Account_ID], [Transaction_Type_ID]) 
	VALUES
		(@Name_Transaction, @Date_Transaction, @Summ_Transaction, @Transaction_Account_ID, @Transaction_Type_ID)

	UPDATE [dbo].[Account]
	SET [Balance] = [Balance] + @Summ_Transaction
	WHERE [ID_Account] = @Transaction_Account_ID
END
GO

EXEC [dbo].[Get_Transactions_By_User] 1
EXEC [dbo].[Execute_Transaction] 'Снятие наличных', -100.00, '40149204400', 2


CREATE OR ALTER PROCEDURE BackupDatabase
@BackupPath NVARCHAR(256),
@Result NVARCHAR(400) OUTPUT
AS
BEGIN
  DECLARE @DatabaseName NVARCHAR(128) = 'Bank_Database'
  
  IF @BackupPath IS NULL
  BEGIN
    SET @BackupPath = 'C:\Backup\'
  END
  
  SET @BackupPath = @BackupPath + '\\' + @DatabaseName + '_BackUpFIle.bak'
  
  BACKUP DATABASE @DatabaseName
  TO DISK =@BackupPath
  WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10;
  SET @Result = 'Полный бэкап бд успешно завершен. Файл сохранён в '+ @BackupPath;
END
