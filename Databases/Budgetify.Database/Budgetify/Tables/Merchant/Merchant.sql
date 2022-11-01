CREATE TABLE [Budgetify].[Merchant]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [UserFk]        INT                 NOT NULL,
    [Name]          NVARCHAR(255)       NOT NULL,
    [CategoryFk]    INT                 NOT NULL,
    CONSTRAINT [FK_Merchant_User]     FOREIGN KEY ([UserFk])     REFERENCES [Budgetify].[User]([Id]),
    CONSTRAINT [FK_Merchant_Category] FOREIGN KEY ([CategoryFk]) REFERENCES [Budgetify].[Category]([Id])
)
