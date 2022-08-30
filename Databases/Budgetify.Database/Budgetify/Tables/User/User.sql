CREATE TABLE [Budgetify].[User]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL, 
    [DeletedOn]     SMALLDATETIME           NULL, 
    [Email]         NVARCHAR(255)       NOT NULL, 
    [FirstName]     NVARCHAR(255)       NOT NULL,
    [LastName]      NVARCHAR(255)       NOT NULL,
    [City]          NVARCHAR(255)       NOT NULL
)
