BEGIN
SET IDENTITY_INSERT [Budgetify].[ExchangeRate] ON
MERGE [Budgetify].[ExchangeRate] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'e6c7e33e-113f-4876-9d95-59ca6cc64616', CAST(N'2022-12-12 15:25:00' AS SMALLDATETIME), NULL, 1, 2, 1, CAST(N'2022-12-13 15:55:00' AS DATETIME2(6)), NULL, 61.53),
        (2, N'242969c8-b19f-4896-aafe-908fa226f705', CAST(N'2022-12-12 15:26:00' AS SMALLDATETIME), NULL, 1, 3, 1, CAST(N'2022-12-13 15:55:00' AS DATETIME2(6)), NULL, 58.26)
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [FromCurrencyFk], [ToCurrencyFk], [FromDate], [ToDate], [Rate])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]            = S.[Uid],
    T.[CreatedOn]      = S.[CreatedOn],
    T.[DeletedOn]      = S.[DeletedOn],
    T.[UserFk]         = S.[UserFk],
    T.[FromCurrencyFk] = S.[FromCurrencyFk],
    T.[ToCurrencyFk]   = S.[ToCurrencyFk],
    T.[FromDate]       = S.[FromDate],
    T.[ToDate]         = S.[ToDate],
    T.[Rate]           = S.[Rate]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [FromCurrencyFk], [ToCurrencyFk], [FromDate], [ToDate], [Rate])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[UserFk], S.[FromCurrencyFk], S.[ToCurrencyFk], S.[FromDate], S.[ToDate], S.[Rate]);
SET IDENTITY_INSERT [Budgetify].[ExchangeRate] OFF
END
