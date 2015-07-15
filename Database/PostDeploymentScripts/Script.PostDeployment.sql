/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[Authors] ON 
GO
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (1, N'Terry Pratchet')
GO
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (2, N'J. K. Rowling')
GO
INSERT [dbo].[Authors] ([Id], [Name]) VALUES (3, N'J. R. R. Tolken')
GO
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO