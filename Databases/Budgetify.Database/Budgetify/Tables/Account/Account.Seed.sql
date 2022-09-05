BEGIN
SET IDENTITY_INSERT [Budgetify].[Account] ON
MERGE [Budgetify].[Account] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'e3c30e16-994e-4f2e-aee7-ac0435eed449', CAST(N'2022-09-04 21:13:00' AS SMALLDATETIME), NULL, 1, N'Cash', N'CASH', 2000, 1, N'Personal cash'),
        (2, N'f4aae1a8-9d01-4ed2-9c99-d7a0b4c3f4be', CAST(N'2022-09-04 21:13:00' AS SMALLDATETIME), NULL, 1, N'Komercijalna Banka', N'DEBIT', 50000, 1, N'Komercijalna banka debit card'),
        (3, N'a503fc32-51ac-4d87-bb2c-9f2a1c4228d9', CAST(N'2022-09-04 21:13:00' AS SMALLDATETIME), NULL, 1, N'NLB', N'DEBIT', 1000, 1, N'NLB debit card'),
        (4, N'83dcbb9a-6dc1-4fec-affd-050e3d632665', CAST(N'2022-09-04 21:13:00' AS SMALLDATETIME), NULL, 1, N'Euro savings', N'SAVINGS', 3000, 2, N'Euro savings account')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [Type], [Balance], [CurrencyFk], [Description])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]         = S.[Uid],
    T.[CreatedOn]   = S.[CreatedOn],
    T.[DeletedOn]   = S.[DeletedOn],
    T.[UserFk]      = S.[UserFk],
    T.[Name]        = S.[Name],
    T.[Type]        = S.[Type],
    T.[Balance]     = S.[Balance],
    T.[CurrencyFk]  = S.[CurrencyFk],
    T.[Description] = S.[Description]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [Type], [Balance], [CurrencyFk], [Description])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[UserFk], S.[Name], S.[Type], S.[Balance], S.[CurrencyFk], S.[Description]);
SET IDENTITY_INSERT [Budgetify].[Account] OFF
END
