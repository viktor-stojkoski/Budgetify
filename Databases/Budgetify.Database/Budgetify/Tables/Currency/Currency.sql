CREATE TABLE [Budgetify].[Currency]
(
    [Id]            INT                 NOT NULL    PRIMARY KEY IDENTITY(1, 1),
    [Uid]           UNIQUEIDENTIFIER    NOT NULL,
    [CreatedOn]     SMALLDATETIME       NOT NULL,
    [DeletedOn]     SMALLDATETIME           NULL,
    [Name]          NVARCHAR(50)        NOT NULL,
    [Code]          NVARCHAR(3)         NOT NULL,
    [Symbol]        NVARCHAR(10)            NULL
)
