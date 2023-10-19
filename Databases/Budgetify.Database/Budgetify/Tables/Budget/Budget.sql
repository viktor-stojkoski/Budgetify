CREATE TABLE [Budgetify].[Budget]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [UserFk]        INT                 NOT NULL,
    [Name]          NVARCHAR(255)       NOT NULL,
    [CategoryFk]    INT                 NOT NULL,
    [CurrencyFk]    INT                 NOT NULL,
    [StartDate]     DATETIME2(0)        NOT NULL,
    [EndDate]       DATETIME2(0)        NOT NULL,
    [Amount]        DECIMAL(19,4)       NOT NULL,
    [AmountSpent]   DECIMAL(19,4)       NOT NULL,
    CONSTRAINT [FK_Budget_User]     FOREIGN KEY ([UserFk])     REFERENCES [Budgetify].[User]([Id]),
    CONSTRAINT [FK_Budget_Category] FOREIGN KEY ([CategoryFk]) REFERENCES [Budgetify].[Category]([Id]),
    CONSTRAINT [FK_Budget_Currency] FOREIGN KEY ([CurrencyFk]) REFERENCES [Budgetify].[Currency]([Id])
)
