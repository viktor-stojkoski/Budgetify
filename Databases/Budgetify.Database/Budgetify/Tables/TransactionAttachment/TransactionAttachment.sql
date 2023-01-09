CREATE TABLE [Budgetify].[TransactionAttachment]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [TransactionFk] INT                 NOT NULL,
    [FilePath]      NVARCHAR(MAX)       NOT NULL,
    [Name]          NVARCHAR(255)       NOT NULL,
    CONSTRAINT [FK_TransactionAttachment_Transaction] FOREIGN KEY ([TransactionFk]) REFERENCES [Budgetify].[Transaction]([Id])
)
