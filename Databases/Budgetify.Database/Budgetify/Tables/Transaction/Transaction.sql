CREATE TABLE [Budgetify].[Transaction]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [UserFk]        INT                 NOT NULL,
    [AccountFk]     INT                 NOT NULL,
    [CategoryFk]    INT                 NOT NULL,
    [CurrencyFk]    INT                 NOT NULL,
    [MerchantFk]    INT                     NULL,
    [Type]          NVARCHAR(50)        NOT NULL,
    [Amount]        DECIMAL(19,4)       NOT NULL,
    [Date]          DATETIME2(0)        NOT NULL,
    [Description]   NVARCHAR(MAX)           NULL,
    CONSTRAINT [FK_Transaction_User]     FOREIGN KEY ([UserFk])     REFERENCES [Budgetify].[User]([Id]),
    CONSTRAINT [FK_Transaction_Account]  FOREIGN KEY ([AccountFk])  REFERENCES [Budgetify].[Account]([Id]),
    CONSTRAINT [FK_Transaction_Category] FOREIGN KEY ([CategoryFk]) REFERENCES [Budgetify].[Category]([Id]),
    CONSTRAINT [FK_Transaction_Currency] FOREIGN KEY ([CurrencyFk]) REFERENCES [Budgetify].[Currency]([Id]),
    CONSTRAINT [FK_Transaction_Merchant] FOREIGN KEY ([MerchantFk]) REFERENCES [Budgetify].[Merchant]([Id])
)
