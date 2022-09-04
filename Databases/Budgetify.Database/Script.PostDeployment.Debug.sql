/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\file.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF ('$(ExecuteSeedData)' = 'Y')
BEGIN
    :r .\Budgetify\Tables\User\User.Seed.sql
    :r .\Budgetify\Tables\Currency\Currency.Seed.sql
END