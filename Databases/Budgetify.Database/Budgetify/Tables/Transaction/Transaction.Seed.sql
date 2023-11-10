BEGIN
SET IDENTITY_INSERT [Budgetify].[Transaction] ON
MERGE [Budgetify].[Transaction] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'f898c84c-28e1-4ff1-9a08-9074697f5ec1', CAST(N'2022-11-07 22:55:00' AS SMALLDATETIME), NULL, 1, 1, NULL, 2, 1, 2, N'EXPENSE', 100, CAST(N'2022-11-07 22:10:00' AS DATETIME2(0)), N'Tea', 1),
        (2, N'7f0992d2-ba8d-4de1-9e5a-38157df0fcdf', CAST(N'2022-11-07 22:55:00' AS SMALLDATETIME), NULL, 1, 1, NULL, 1, 1, 1, N'EXPENSE', 2000, CAST(N'2022-11-07 18:30:00' AS DATETIME2(0)), NULL, 1),
        (3, N'a3282906-1123-4df2-bae8-cedb4b8d5e7e', CAST(N'2022-11-07 22:56:00' AS SMALLDATETIME), NULL, 1, 2, NULL, 4, 1, 4, N'EXPENSE', 528, CAST(N'2022-11-07 12:10:00' AS DATETIME2(0)), N'Groceries', 1),
        (4, N'ee3c7f43-eeea-46b9-bcf2-af86bc911e58', CAST(N'2022-11-07 22:56:00' AS SMALLDATETIME), NULL, 1, 2, NULL, 3, 1, NULL, N'INCOME', 55000, CAST(N'2022-10-31 14:15:00' AS DATETIME2(0)), N'Salary', 1)
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [AccountFk], [FromAccountFk], [CategoryFk], [CurrencyFk], [MerchantFk], [Type], [Amount], [Date], [Description], [IsVerified])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]           = S.[Uid],
    T.[CreatedOn]     = S.[CreatedOn],
    T.[DeletedOn]     = S.[DeletedOn],
    T.[UserFk]        = S.[UserFk],
    T.[AccountFk]     = S.[AccountFk],
    T.[FromAccountFk] = S.[FromAccountFk],
    T.[CategoryFk]    = S.[CategoryFk],
    T.[CurrencyFk]    = S.[CurrencyFk],
    T.[MerchantFk]    = S.[MerchantFk],
    T.[Type]          = S.[Type],
    T.[Amount]        = S.[Amount],
    T.[Date]          = S.[Date],
    T.[Description]   = S.[Description],
    T.[IsVerified]    = S.[IsVerified]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id],
     [Uid],
     [CreatedOn],
     [DeletedOn],
     [UserFk],
     [AccountFk],
     [FromAccountFk],
     [CategoryFk],
     [CurrencyFk],
     [MerchantFk],
     [Type],
     [Amount],
     [Date],
     [Description],
     [IsVerified])
VALUES
    (S.[Id], 
     S.[Uid], 
     S.[CreatedOn],
     S.[DeletedOn],
     S.[UserFk],
     S.[AccountFk],
     S.[FromAccountFk],
     S.[CategoryFk],
     S.[CurrencyFk],
     S.[MerchantFk],
     S.[Type],
     S.[Amount],
     S.[Date],
     S.[Description],
     S.[IsVerified]);
SET IDENTITY_INSERT [Budgetify].[Transaction] OFF
END
