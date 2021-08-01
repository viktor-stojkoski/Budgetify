BEGIN
SET IDENTITY_INSERT [dbo].[Test] ON
MERGE [dbo].[Test] AS T
USING
(
    SELECT *
    FROM (
    VALUES 
           (1, N'8ab35bf2-5bad-4717-8521-75a1f42d6188', CAST(N'2010-05-05 10:37:00' AS SMALLDATETIME), NULL, N'Name 1', N'Address 1'),
           (2, N'e68c7744-ba5a-4789-a09e-fba9c86aecd5', CAST(N'2010-05-05 10:37:00' AS SMALLDATETIME), NULL, N'Name 2', N'Address 2'),
           (3, N'50b045db-6420-4e4b-b475-143811f8240b', CAST(N'2010-05-05 10:37:00' AS SMALLDATETIME), NULL, N'Name 3', N'Address 3'),
           (4, N'04256d79-5bd8-41be-a90c-e13a4ab5d343', CAST(N'2010-05-05 10:37:00' AS SMALLDATETIME), NULL, N'Name 4', N'Address 4'),
           (5, N'fad55626-9765-4576-9ef5-e5df4aaae5a3', CAST(N'2010-05-05 10:37:00' AS SMALLDATETIME), NULL, N'Name 5', N'Address 5')
     ) AS temp ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Address])
) AS S
ON T.Id=S.Id
WHEN MATCHED THEN UPDATE SET
      T.[Uid] = S.[Uid],
      T.[CreatedOn] = S.[CreatedOn],
      T.[DeletedOn] = S.[DeletedOn],
      T.[Name] = S.[Name],
      T.[Address] = S.[Address]
WHEN NOT MATCHED THEN 
INSERT 
    ([Id], [Uid], [CreatedOn], [DeletedOn], [Name], [Address])
VALUES
    (S.[Id], S.[Uid], S.[CreatedOn], S.[DeletedOn], S.[Name], S.[Address]);
SET IDENTITY_INSERT [dbo].[Test] OFF
END
