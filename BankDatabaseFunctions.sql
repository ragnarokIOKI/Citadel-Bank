use [Bank_Database]
go

CREATE OR ALTER FUNCTION [dbo].[Get_Total_Credit_Application_Amount] (@User_ID [int])
RETURNS TABLE
AS
RETURN (
  SELECT [Application_Amount] as "Изначальная сумма кред.договора",
         [Credit_Rate] as "Процент кредита",
         DATEDIFF(year, [Date_Drawing], [Date_Termination]) AS "Время для выплаты кредита в годах",
         dbo.Calculate_Overpayment(
          Application_Amount,
          Credit_Rate,
          Date_Drawing,
          Date_Termination
         ) AS "Переплата по кредиту"
  FROM [dbo].[Credit_Agreement]
  INNER JOIN [dbo].[Credit_Application] ON [Credit_Application_ID] = [ID_Credit_Application]
  WHERE [CreditApplication_User_ID] = @User_ID
)
GO

select * from [dbo].[Get_Total_Credit_Application_Amount] (1)
go

create or alter function [dbo].[Calculate_Overpayment]
(@Credit_Amount DECIMAL(19, 2), @Credit_Rate DECIMAL(38, 2), @Date_Drawing DATE, @Date_Termination DATE)
RETURNS DECIMAL(19, 2)
AS
BEGIN
   DECLARE @Monthly_Interest_Rate DECIMAL(19, 6) = @Credit_Rate / 1200;
   DECLARE @Duration_In_Months INTEGER = DATEDIFF(MONTH, @Date_Drawing, @Date_Termination);
   DECLARE @Monthly_Payment DECIMAL(19, 2) = (@Credit_Amount * @Monthly_Interest_Rate * POWER(1 + @Monthly_Interest_Rate, @Duration_In_Months)) 
                                             / (POWER(1 + @Monthly_Interest_Rate, @Duration_In_Months) - 1);
   DECLARE @Total_Payment DECIMAL(19, 2) = @Monthly_Payment * @Duration_In_Months;
   DECLARE @Overpayment DECIMAL(19, 2) = @Total_Payment - @Credit_Amount;

   RETURN @Overpayment;
END;

CREATE OR ALTER FUNCTION [dbo].[Calculate_Overpayment]
(
    @Credit_Amount DECIMAL(19, 2),
    @Credit_Rate DECIMAL(38, 2),
    @Date_Drawing DATE,
    @Date_Termination DATE
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        @Credit_Amount AS Credit_Amount,
        @Credit_Rate AS Credit_Rate,
        @Date_Drawing AS Date_Drawing,
        @Date_Termination AS Date_Termination,
        (
            SELECT
                (
                    @Credit_Amount * Monthly_Interest_Rate * POWER(1 + Monthly_Interest_Rate, Duration_In_Months)
                ) / (
                    POWER(1 + Monthly_Interest_Rate, Duration_In_Months) - 1
                ) * Duration_In_Months - @Credit_Amount AS Overpayment
            FROM (
                SELECT
                    DATEDIFF(MONTH, @Date_Drawing, @Date_Termination) AS Duration_In_Months,
                    @Credit_Rate / 1200 AS Monthly_Interest_Rate
            ) AS OverpaymentCalculation
        ) AS Overpayment
);

CREATE OR ALTER FUNCTION [dbo].[Calculate_Overpayment]
(
    @Credit_Amount DECIMAL(19, 2),
    @Credit_Rate DECIMAL(38, 2),
    @Date_Drawing DATE,
    @Date_Termination DATE
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        @Credit_Amount AS Credit_Amount,
        @Credit_Rate AS Credit_Rate,
        @Date_Drawing AS Date_Drawing,
        @Date_Termination AS Date_Termination,
        (
            SELECT
                (
                    @Credit_Amount * Monthly_Interest_Rate * POWER(1 + Monthly_Interest_Rate, CAST(Duration_In_Months AS DECIMAL(10, 2)))
                ) / (
                    POWER(1 + Monthly_Interest_Rate, CAST(Duration_In_Months AS DECIMAL(10, 2))) - 1
                ) * CAST(Duration_In_Months AS DECIMAL(10, 2)) - @Credit_Amount AS Overpayment
            FROM (
                SELECT
                    CAST(DATEDIFF(MONTH, @Date_Drawing, @Date_Termination) AS DECIMAL(10, 2)) AS Duration_In_Months,
                    @Credit_Rate / 1200 AS Monthly_Interest_Rate
            ) AS OverpaymentCalculation
        ) AS Overpayment
);

select * from [dbo].[Calculate_Overpayment] (1000000.0, 4.4, '2023-07-21', '2033-07-21')
go