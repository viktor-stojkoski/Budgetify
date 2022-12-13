CREATE TABLE [Budgetify].[ExchangeRate]
(
    [Id]                INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]               UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]         SMALLDATETIME       NOT NULL,
    [DeletedOn]         SMALLDATETIME           NULL,
    [UserFk]            INT                 NOT NULL,
    [FromCurrencyFk]    INT                 NOT NULL,
    [ToCurrencyFk]      INT                 NOT NULL,
    [FromDate]          DATETIME2(6)            NULL,
    [ToDate]            DATETIME2(6)            NULL,
    [Rate]              DECIMAL(10,4)       NOT NULL,
    CONSTRAINT [FK_ExchangeRate_User]         FOREIGN KEY ([UserFk])         REFERENCES [Budgetify].[User]([Id]),
    CONSTRAINT [FK_ExchangeRate_FromCurrency] FOREIGN KEY ([FromCurrencyFk]) REFERENCES [Budgetify].[Currency]([Id]),
    CONSTRAINT [FK_ExchangeRate_ToCurrency]   FOREIGN KEY ([ToCurrencyFk])   REFERENCES [Budgetify].[Currency]([Id]),
)
