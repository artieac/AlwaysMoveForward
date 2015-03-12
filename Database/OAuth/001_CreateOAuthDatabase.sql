USE [AMForwardDb]
GO
/****** Object:  Table [dbo].[LoginAttempts]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginAttempts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WasSuccessfull] [bit] NOT NULL,
	[AttemptDate] [datetime2](7) NOT NULL,
	[Source] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_LoginAttempts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_LoginAttempts_UserName] ON [dbo].[LoginAttempts] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consumers]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consumers](
	[ConsumerKey] [nvarchar](36) NOT NULL,
	[ConsumerSecret] [nvarchar](36) NOT NULL,
	[PublicKey] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ContactEmail] [nvarchar](255) NOT NULL,
	[AutoGrant] [bit] NOT NULL,
	[AccessTokenLifetime] [int] NOT NULL,
 CONSTRAINT [PK_Consumers] PRIMARY KEY CLUSTERED 
(
	[ConsumerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsumerNonce]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsumerNonce](
	[Nonce] [nvarchar](36) NOT NULL,
	[ConsumerKey] [nvarchar](36) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_ConsumerNonce_1] PRIMARY KEY CLUSTERED 
(
	[Nonce] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AMFUsers]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AMFUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[PasswordSalt] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](50) NOT NULL,
	[PasswordHint] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[UserStatus] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[ResetToken] [nvarchar](36) NULL,
 CONSTRAINT [PK_AMFUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AMFUsers_Email] ON [dbo].[AMFUsers] 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessTokens]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessTokens](
	[ConsumerKey] [nvarchar](36) NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[Realm] [nvarchar](512) NOT NULL,
	[Token] [nvarchar](36) NOT NULL,
	[TokenSecret] [nvarchar](36) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DateGranted] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AccessTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AccessTokens_Token] ON [dbo].[AccessTokens] 
(
	[Token] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestTokens]    Script Date: 03/12/2015 12:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsumerKey] [nvarchar](36) NOT NULL,
	[Realm] [nvarchar](512) NULL,
	[Token] [nvarchar](36) NOT NULL,
	[TokenSecret] [nvarchar](36) NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[CallbackUrl] [nvarchar](max) NOT NULL,
	[State] [int] NOT NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](255) NULL,
	[VerifierCode] [nvarchar](36) NULL,
	[DateAuthorized] [datetime2](7) NULL,
	[AccessTokenId] [int] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RequestTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestTokens_Token] ON [dbo].[RequestTokens] 
(
	[Token] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_AccessTokens_AccessTokens]    Script Date: 03/12/2015 12:16:42 ******/
ALTER TABLE [dbo].[AccessTokens]  WITH CHECK ADD  CONSTRAINT [FK_AccessTokens_AccessTokens] FOREIGN KEY([Id])
REFERENCES [dbo].[AccessTokens] ([Id])
GO
ALTER TABLE [dbo].[AccessTokens] CHECK CONSTRAINT [FK_AccessTokens_AccessTokens]
GO
/****** Object:  ForeignKey [FK_RequestTokens_AccessTokens]    Script Date: 03/12/2015 12:16:42 ******/
ALTER TABLE [dbo].[RequestTokens]  WITH CHECK ADD  CONSTRAINT [FK_RequestTokens_AccessTokens] FOREIGN KEY([AccessTokenId])
REFERENCES [dbo].[AccessTokens] ([Id])
GO
ALTER TABLE [dbo].[RequestTokens] CHECK CONSTRAINT [FK_RequestTokens_AccessTokens]
GO
