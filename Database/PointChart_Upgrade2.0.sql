USE [AMForwardDb]
GO
/****** Object:  Table [dbo].[PointEarners]    Script Date: 07/05/2012 10:26:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointEarners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[AdministratorId] [int] NOT NULL,
	[PointsEarned] [float] NOT NULL,
	[PointsSpent] [float] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PointEarners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/** Copy Information into Point Earners from Charts **/
INSERT INTO PointEarners(FirstName, LastName, Email, AdministratorId, PointsEarned, PointsSpent, UserName, Password) 
SELECT FirstName, LastName, Email, AdministratorId, PointsEarned, PointsSpent, UserName, Password From Charts;
GO
/** Update Charts Table to have PointEarnerId **/
ALTER TABLE [dbo].[Charts] ADD [PointEarnerId] [int];
GO
/** Update PointEarnerId Column **/
UPDATE Charts SET PointEarnerId = Id;
GO
/** Make PointEarnerId not allow nulls **/
ALTER TABLE [dbo].[Charts] ALTER COLUMN [PointEarnerId] [int] NOT NULL;
GO
/** Add in the Name column not allow nulls **/
ALTER TABLE [dbo].[Charts] ADD [Name] [nvarchar](100);
GO
/** Update Name Column **/
UPDATE Charts SET Name = FirstName;
GO
/** Make Name not allow nulls **/
ALTER TABLE [dbo].[Charts] ALTER COLUMN [Name] [nvarchar](100) NOT NULL;
GO
/****** Object:  Table [dbo].[PointsSpent]    Script Date: 07/05/2012 10:26:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointsSpent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PointEarnerId] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[DateSpent] date NOT NULL,
	[Amount] [float] NOT NULL,
 CONSTRAINT [PK_PointsSpent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO