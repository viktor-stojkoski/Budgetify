CREATE TABLE [Budgetify].[Category]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [UserFk]        INT                 NOT NULL,
    [Name]          NVARCHAR(255)       NOT NULL,
    [Type]          NVARCHAR(50)        NOT NULL    DEFAULT N'',
    CONSTRAINT [FK_Category_User] FOREIGN KEY ([UserFk]) REFERENCES [Budgetify].[User]([Id]),
)
