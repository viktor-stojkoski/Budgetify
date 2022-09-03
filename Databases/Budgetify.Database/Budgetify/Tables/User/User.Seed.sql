BEGIN
SET IDENTITY_INSERT [Budgetify].[User] ON
MERGE [Budgetify].[User] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'63074caf-552a-4ab4-931f-7882cb946e6f', CAST(N'2022-04-27 15:07:00' AS SMALLDATETIME), NULL, N'viktor@budgetify.tech', N'Viktor', N'Stojkoski', N'Skopje'),
        (2, N'f95d146e-124e-471d-b2ff-fbff0e22c086', CAST(N'2022-04-27 15:08:00' AS SMALLDATETIME), NULL, N'zoran@budgetify.tech', N'Zoran', N'Zoranovski', N'Gostivar')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [Email], [FirstName], [LastName], [City])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]       = S.[Uid],
    T.[CreatedOn] = S.[CreatedOn],
    T.[DeletedOn] = S.[DeletedOn],
    T.[Email]     = S.[Email],
    T.[FirstName] = S.[FirstName],
    T.[LastName]  = S.[LastName],
    T.[City]      = S.[City]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [Email], [FirstName], [LastName], [City])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[Email], S.[FirstName], S.[LastName], S.[City]);
SET IDENTITY_INSERT [Budgetify].[User] OFF
END
