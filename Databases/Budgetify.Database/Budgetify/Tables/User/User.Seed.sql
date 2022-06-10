BEGIN
SET IDENTITY_INSERT [Budgetify].[User] ON
MERGE [Budgetify].[User] AS T
USING
(
    SELECT *
    FROM (
    VALUES 
           (1, N'63074caf-552a-4ab4-931f-7882cb946e6f', CAST(N'2022-04-27 15:07:00' AS SMALLDATETIME), NULL, N'Viktor', N'viktor@budgetify.tech'),
           (2, N'f95d146e-124e-471d-b2ff-fbff0e22c086', CAST(N'2022-04-27 15:08:00' AS SMALLDATETIME), NULL, N'Zoran', N'zoran@budgetify.tech')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Email])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
      T.[Uid] = S.[Uid],
      T.[CreatedOn] = S.[CreatedOn],
      T.[DeletedOn] = S.[DeletedOn],
      T.[Name] = S.[Name],
      T.[Email] = S.[Email]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Email])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[Name], S.[Email]);
SET IDENTITY_INSERT [Budgetify].[User] OFF
END
