BEGIN
SET IDENTITY_INSERT [Budgetify].[Budget] ON
MERGE [Budgetify].[Budget] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'a00f8acb-9ddd-4620-8015-fcf5dd00c970', CAST(N'2023-06-11 16:17:00' AS SMALLDATETIME), NULL, 1, N'Gas', 1, CAST(N'2023-06-11 16:17:00' AS DATETIME2(0)), CAST(N'2023-06-30 22:10:00' AS DATETIME2(0)), 5000, 3000),
        (2, N'4f3c263f-763b-415e-b425-2011c060171f', CAST(N'2023-06-11 16:17:00' AS SMALLDATETIME), NULL, 1, N'Restaurants', 2, CAST(N'2023-06-11 16:17:00' AS DATETIME2(0)), CAST(N'2023-06-30 22:10:00' AS DATETIME2(0)), 10000, 2500)
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [CategoryFk], [StartDate], [EndDate], [Amount], [AmountSpent])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]         = S.[Uid],
    T.[CreatedOn]   = S.[CreatedOn],
    T.[DeletedOn]   = S.[DeletedOn],
    T.[UserFk]      = S.[UserFk],
    T.[Name]        = S.[Name],
    T.[CategoryFk]  = S.[CategoryFk],
    T.[StartDate]   = S.[StartDate],
    T.[EndDate]     = S.[EndDate],
    T.[Amount]      = S.[Amount],
    T.[AmountSpent] = S.[AmountSpent]
WHEN NOT MATCHED THEN
INSERT
    ([Id],
     [Uid],
     [CreatedOn],
     [DeletedOn],
     [UserFk],
     [Name],
     [CategoryFk],
     [StartDate],
     [EndDate],
     [Amount],
     [AmountSpent])
VALUES
    (S.[Id],
     S.[Uid],
     S.[CreatedOn],
     S.[DeletedOn],
     S.[UserFk],
     S.[Name],
     S.[CategoryFk],
     S.[StartDate],
     S.[EndDate],
     S.[Amount],
     S.[AmountSpent]);
SET IDENTITY_INSERT [Budgetify].[Budget] OFF
END
