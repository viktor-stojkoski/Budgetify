CREATE TABLE [Budgetify].[Account]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [UserFk]        INT                 NOT NULL,
    [Name]          NVARCHAR(255)       NOT NULL,
    [Type]          NVARCHAR(50)        NOT NULL,
    [Balance]       DECIMAL(19,4)       NOT NULL    DEFAULT 0,
    [CurrencyFk]    INT                 NOT NULL,
    [Description]   NVARCHAR(MAX)           NULL    DEFAULT N'',
    CONSTRAINT [FK_Account_User]     FOREIGN KEY ([UserFk]) REFERENCES [Budgetify].[User]([Id]),
    CONSTRAINT [FK_Account_Currency] FOREIGN KEY ([UserFk]) REFERENCES [Budgetify].[Currency]([Id])
)
