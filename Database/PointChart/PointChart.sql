USE [PointChartDb]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsSiteAdministrator] [bit] NOT NULL,
	[About] [text] NULL,
	[AccessToken] [nvarchar](36) NULL,
	[AccessTokenSecret] [nvarchar](36) NULL,
	[OAuthServiceUserId] [bigint] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Points] [float] NOT NULL,
	[MaxAllowedDaily] [int] NOT NULL,
	[CreatorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PointsSpent]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointsSpent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PointEarnerId] [bigint] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[DateSpent] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
 CONSTRAINT [PK_PointsSpent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompletedTasks]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompletedTasks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskId] [bigint] NOT NULL,
	[DateCompleted] [datetime] NOT NULL,
	[NumberOfTimesCompleted] [int] NOT NULL,
	[CreatorId] [bigint] NOT NULL,
	[ChartId] [bigint] NOT NULL,
	[PointValue] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChartTasks]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartTasks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ChartId] [bigint] NOT NULL,
	[TaskId] [bigint] NOT NULL,
 CONSTRAINT [PK_ChartTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Charts]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Charts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatorId] [bigint] NOT NULL,
	[PointEarnerId] [bigint] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Charts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Users_IsSiteAdministrator]    Script Date: 03/19/2015 23:08:26 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsSiteAdministrator]  DEFAULT ((0)) FOR [IsSiteAdministrator]
GO
/****** Object:  Table [dbo].[PointEarners]    Script Date: 03/19/2015 23:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointEarners](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PointEarnerId] [bigint] NOT NULL,
 CONSTRAINT [PK_PointEarners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO