BEGIN
SET IDENTITY_INSERT [Budgetify].[Category] ON
MERGE [Budgetify].[Category] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'ef0b7cda-0f54-4647-aee4-3b7eeefc6c5d', CAST(N'2022-10-27 15:46:00' AS SMALLDATETIME), NULL, 1, N'Gas', N'EXPENSE'),
        (2, N'14e1c1eb-9555-4763-be5d-7dcba80fcb35', CAST(N'2022-10-27 15:47:00' AS SMALLDATETIME), NULL, 1, N'Coffee', N'EXPENSE'),
        (3, N'12652fb3-8e39-4f9d-9720-0259a5d3a313', CAST(N'2022-10-27 15:50:00' AS SMALLDATETIME), NULL, 1, N'Salary', N'INCOME')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [Type])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]       = S.[Uid],
    T.[CreatedOn] = S.[CreatedOn],
    T.[DeletedOn] = S.[DeletedOn],
    T.[UserFk]    = S.[UserFk],
    T.[Name]      = S.[Name],
    T.[Type]      = S.[Type]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [Type])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[UserFk], S.[Name], S.[Type]);
SET IDENTITY_INSERT [Budgetify].[Category] OFF
END
