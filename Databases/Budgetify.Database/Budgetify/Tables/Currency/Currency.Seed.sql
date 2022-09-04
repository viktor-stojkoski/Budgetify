BEGIN
SET IDENTITY_INSERT [Budgetify].[Currency] ON
MERGE [Budgetify].[Currency] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'755c0c95-1fdf-4b2c-ac34-452038ae59d3', CAST(N'2022-09-03 14:58:00' AS SMALLDATETIME), NULL, N'Macedonian Denar', N'MKD', N'ден'),
        (2, N'6c05c614-e9d8-4576-bb88-8245f41d5fed', CAST(N'2022-09-03 14:59:00' AS SMALLDATETIME), NULL, N'Euro', N'EUR', N'€'),
        (3, N'0fec0740-29a7-403c-bbd2-1b05f669d17e', CAST(N'2022-09-03 15:04:00' AS SMALLDATETIME), NULL, N'United States Dollar', N'USD', N'$')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Code], [Symbol])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]       = S.[Uid],
    T.[CreatedOn] = S.[CreatedOn],
    T.[DeletedOn] = S.[DeletedOn],
    T.[Name]      = S.[Name],
    T.[Code]      = S.[Code],
    T.[Symbol]    = S.[Symbol]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Code], [Symbol])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[Name], S.[Code], S.[Symbol]);
SET IDENTITY_INSERT [Budgetify].[Currency] OFF
END
