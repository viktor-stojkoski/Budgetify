BEGIN
SET IDENTITY_INSERT [Budgetify].[Merchant] ON
MERGE [Budgetify].[Merchant] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'47f7d365-afe4-47e3-86d5-8b04260769b2', CAST(N'2022-11-01 22:08:00' AS SMALLDATETIME), NULL, 1, N'Makpetrol', 1),
        (2, N'f142cefc-3503-4477-87e6-499a18df8a86', CAST(N'2022-11-01 22:08:00' AS SMALLDATETIME), NULL, 1, N'Revija', 2),
        (3, N'38a52fb4-7dfc-43d9-81df-b5ac488c583a', CAST(N'2022-11-01 22:08:00' AS SMALLDATETIME), NULL, 1, N'Ramstore', 4),
        (4, N'74ab4e99-a2ee-4422-be66-07fe836e5d46', CAST(N'2022-11-01 22:09:00' AS SMALLDATETIME), NULL, 1, N'Stokomak', 4)
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [CategoryFk])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]         = S.[Uid],
    T.[CreatedOn]   = S.[CreatedOn],
    T.[DeletedOn]   = S.[DeletedOn],
    T.[UserFk]      = S.[UserFk],
    T.[Name]        = S.[Name],
    T.[CategoryFk]  = S.[CategoryFk]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [UserFk], [Name], [CategoryFk])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[UserFk], S.[Name], S.[CategoryFk]);
SET IDENTITY_INSERT [Budgetify].[Merchant] OFF
END
