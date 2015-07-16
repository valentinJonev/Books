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
USE [Books]
GO

/****** Object:  Table [dbo].[Authors]    Script Date: 16.7.2015 г. 11:26:27 ч. ******/
DROP TABLE [dbo].[Authors]
GO

/****** Object:  Table [dbo].[Authors]    Script Date: 16.7.2015 г. 11:26:27 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

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

SET ANSI_PADDING ON
GO

