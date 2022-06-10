CREATE TABLE [Budgetify].[User]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL, 
    [DeletedOn]     SMALLDATETIME           NULL, 
    [Email]         NVARCHAR(255)       NOT NULL, 
    [Name]          NVARCHAR(255)       NOT NULL
)
