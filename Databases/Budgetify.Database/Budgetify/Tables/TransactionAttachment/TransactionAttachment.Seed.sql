BEGIN
SET IDENTITY_INSERT [Budgetify].[TransactionAttachment] ON
MERGE [Budgetify].[TransactionAttachment] AS T
USING
(
    SELECT *
    FROM (
    VALUES
        (1, N'43296a29-161e-4482-9f4e-386403d82cc6', CAST(N'2023-01-09 14:43:00' AS SMALLDATETIME), NULL, 3, N'https://www.snopes.com/tachyon/2021/08/239918331_10228097135359041_3825446756894757753_n.jpg', N'Receipt1'),
        (2, N'1ebf1c8e-4eea-42ea-8e11-565888be9953', CAST(N'2023-01-09 14:44:00' AS SMALLDATETIME), NULL, 3, N'https://templates.invoicehome.com/invoice-template-us-neat-750px.png', N'Invoice1'),
        (3, N'188d3480-573b-4977-9396-349f5e9612d4', CAST(N'2023-01-09 14:44:00' AS SMALLDATETIME), NULL, 1, N'https://i.pinimg.com/736x/d8/05/36/d80536b691e0af13264f43298e7aa749.jpg', N'Receipt2')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [TransactionFk], [FilePath], [Name])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
    T.[Uid]           = S.[Uid],
    T.[CreatedOn]     = S.[CreatedOn],
    T.[DeletedOn]     = S.[DeletedOn],
    T.[TransactionFk] = S.[TransactionFk],
    T.[FilePath]      = S.[FilePath],
    T.[Name]          = S.[Name]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [TransactionFk], [FilePath], [Name])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[TransactionFk], S.[FilePath], S.[Name]);
SET IDENTITY_INSERT [Budgetify].[TransactionAttachment] OFF
END
