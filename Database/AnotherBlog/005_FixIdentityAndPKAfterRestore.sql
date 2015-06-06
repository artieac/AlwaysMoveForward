USE [AlwaysMoveForward]
GO
/****** Object:  Table [dbo].[SiteInfo]    Script Date: 09/15/2014 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[SiteInfo] ADD CONSTRAINT [PK_SiteInfo] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/15/2014 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Roles] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT PK_Roles
GO
ALTER TABLE dbo.BlogUsers DROP CONSTRAINT FK_BlogUsers_Roles
GO
ALTER TABLE [dbo].[Roles] DROP COLUMN RoleId
GO
EXEC sp_RENAME 'Roles.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollQuestions]    Script Date: 09/15/2014 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[PollQuestions] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[PollQuestions] DROP CONSTRAINT PK_PollQuestions
GO
ALTER TABLE [dbo].[PollQuestions] DROP COLUMN PollQuestionId
GO
EXEC sp_RENAME 'PollQuestions.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE CONSTRAINT [PK_PollQuestions] ADD CONSTRAINT PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollOptions]    Script Date: 09/15/2014 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[PollOptions] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[PollOptions] DROP CONSTRAINT PK_PollOptions
GO
ALTER TABLE [dbo].[PollOptions] DROP COLUMN PollOptionId
GO
EXEC sp_RENAME 'PollOptions.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[PollOptions] ADD CONSTRAINT [PK_PollOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PointsSpent]    Script Date: 09/15/2014 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[BlogLists] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogLists] DROP CONSTRAINT BlogLists_PrimaryKey
GO
ALTER TABLE [dbo].[BlogLists] DROP COLUMN Id
GO
EXEC sp_RENAME 'BlogLists.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogLists] ADD CONSTRAINT [BlogLists_PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BlogListItems] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogListItems] DROP CONSTRAINT BlogListItems_PrimaryKey
GO
ALTER TABLE [dbo].[BlogListItems] DROP COLUMN Id
GO
EXEC sp_RENAME 'BlogListItems.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogListItems] ADD CONSTRAINT [BlogListItems_PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogExtensions] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogExtensions] DROP CONSTRAINT BlogExtensions_PrimaryKey
GO
ALTER TABLE [dbo].[BlogExtensions] DROP COLUMN ExtensionId
GO
EXEC sp_RENAME 'BlogExtensions.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogExtensions] ADD CONSTRAINT [BlogExtensions_PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ExtensionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blogs] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT PK_Blogs
GO
ALTER TABLE [dbo].[Blogs] DROP COLUMN BlogId
GO
EXEC sp_RENAME 'Blogs.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[Blogs] ADD CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VoterAddresses] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[VoterAddresses] DROP CONSTRAINT PK_VoterAddresses
GO
ALTER TABLE [dbo].[VoterAddresses] DROP COLUMN VoterAddressId
GO
EXEC sp_RENAME 'Blogs.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[VoterAddresses] ADD CONSTRAINT [PK_VoterAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD [NewIdField] [bigint] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT PK_Users
GO
ALTER TABLE [dbo].[Users] DROP COLUMN UserId
GO
EXEC sp_RENAME 'Users.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogEntries] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogEntries] DROP CONSTRAINT PK_BlogEntries
GO
ALTER TABLE [dbo].[BlogEntries] DROP COLUMN EntryId
GO
EXEC sp_RENAME 'BlogEntries.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogEntries] ADD CONSTRAINT [PK_BlogEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BlogEntries] ON [dbo].[BlogEntries] 
(
	[BlogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Tags_BlogId] ON [dbo].[Tags] 
(
	[BlogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogUsers]    Script Date: 09/15/2014 08:05:14 ******/
GO
ALTER TABLE [dbo].[BlogUsers] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogUsers] DROP CONSTRAINT PK_BlogUsers
GO
ALTER TABLE [dbo].[BlogUsers] DROP COLUMN BlogUserId
GO
EXEC sp_RENAME 'BlogUsers.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogUsers] ADD CONSTRAINT [PK_BlogUsers] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BlogUsers_BlogId] ON [dbo].[BlogUsers] 
(
	[BlogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BlogUsers_UserId] ON [dbo].[BlogUsers] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogRollLinks] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogRollLinks] DROP CONSTRAINT PK_BlogRollLinks
GO
ALTER TABLE [dbo].[BlogRollLinks] DROP COLUMN BlogRollLinkId
GO
EXEC sp_RENAME 'BlogRollLinks.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogRollLinks] CONSTRAINT [PK_BlogRollLinks] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BlogRollLinks] ON [dbo].[BlogRollLinks] 
(
	[BlogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntryComments] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[EntryComments] DROP CONSTRAINT PK_EntryComments
GO
ALTER TABLE [dbo].[EntryComments] DROP COLUMN CommentId
GO
EXEC sp_RENAME 'EntryComments.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[EntryComments] ADD CONSTRAINT [PK_EntryComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntryComments_EntryId] ON [dbo].[EntryComments] 
(
	[EntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogEntryTags] ADD [NewIdField] [int] IDENTITY(1,1)
GO
ALTER TABLE [dbo].[BlogEntryTags] DROP CONSTRAINT PK_EntryComments
GO
ALTER TABLE [dbo].[BlogEntryTags] DROP COLUMN BlogEntryTagId
GO
EXEC sp_RENAME 'BlogEntryTags.NewIdField' , 'Id', 'COLUMN'
GO
ALTER TABLE [dbo].[BlogEntryTags] CONSTRAINT [PK_BlogEntryTags] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BlogEntryTags] ON [dbo].[BlogEntryTags] 
(
	[BlogEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Default [DF_Blogs_CurrentPollId]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Blogs] ADD  CONSTRAINT [DF_Blogs_CurrentPollId]  DEFAULT ((-1)) FOR [CurrentPollId]
GO
/****** Object:  Default [DF_Users_ApprovedCommenter]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ApprovedCommenter]  DEFAULT ((0)) FOR [ApprovedCommenter]
GO
/****** Object:  Default [DF_Users_IsActive]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_Users_IsSiteAdministrator]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsSiteAdministrator]  DEFAULT ((0)) FOR [IsSiteAdministrator]
GO
/****** Object:  Default [DF_BlogEntries_IsPublished]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries] ADD  CONSTRAINT [DF_BlogEntries_IsPublished]  DEFAULT ((0)) FOR [IsPublished]
GO
/****** Object:  Default [DF_BlogEntries_DatePosted]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries] ADD  CONSTRAINT [DF_BlogEntries_DatePosted]  DEFAULT (((1)/(1))/(2000)) FOR [DatePosted]
GO
/****** Object:  Default [[dbo]].[BlogEntries]]DateCreatedDefault]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries] ADD  CONSTRAINT [[dbo]].[BlogEntries]]DateCreatedDefault]  DEFAULT (((1)/(1))/(2009)) FOR [DateCreated]
GO
/****** Object:  Default [[dbo]].[BlogEntries]]TimesViewedDefault]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries] ADD  CONSTRAINT [[dbo]].[BlogEntries]]TimesViewedDefault]  DEFAULT ((0)) FOR [TimesViewed]
GO
/****** Object:  Default [DF_BlogUsers_RoleId]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogUsers] ADD  CONSTRAINT [DF_BlogUsers_RoleId]  DEFAULT ((3)) FOR [RoleId]
GO
/****** Object:  Default [DF_EntryComments_Approved]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[EntryComments] ADD  CONSTRAINT [DF_EntryComments_Approved]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_EntryComments_DatePosted]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[EntryComments] ADD  CONSTRAINT [DF_EntryComments_DatePosted]  DEFAULT (((1)/(1))/(2000)) FOR [DatePosted]
GO
/****** Object:  ForeignKey [FK_BlogEntries_Blogs]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries]  WITH CHECK ADD  CONSTRAINT [FK_BlogEntries_Blogs] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[BlogEntries] CHECK CONSTRAINT [FK_BlogEntries_Blogs]
GO
/****** Object:  ForeignKey [FK_BlogEntries_Users]    Script Date: 09/15/2014 08:05:13 ******/
ALTER TABLE [dbo].[BlogEntries]  WITH CHECK ADD  CONSTRAINT [FK_BlogEntries_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BlogEntries] CHECK CONSTRAINT [FK_BlogEntries_Users]
GO
/****** Object:  ForeignKey [FK_Tags_Blogs]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[Tags]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Blogs] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[Tags] CHECK CONSTRAINT [FK_Tags_Blogs]
GO
/****** Object:  ForeignKey [FK_BlogUsers_Blogs]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlogUsers_Blogs] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[BlogUsers] CHECK CONSTRAINT [FK_BlogUsers_Blogs]
GO
/****** Object:  ForeignKey [FK_BlogUsers_Roles]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlogUsers_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[BlogUsers] CHECK CONSTRAINT [FK_BlogUsers_Roles]
GO
/****** Object:  ForeignKey [FK_BlogUsers_Users]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlogUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BlogUsers] CHECK CONSTRAINT [FK_BlogUsers_Users]
GO
/****** Object:  ForeignKey [FK_BlogLinks_Blogs]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogRollLinks]  WITH CHECK ADD  CONSTRAINT [FK_BlogLinks_Blogs] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO
ALTER TABLE [dbo].[BlogRollLinks] CHECK CONSTRAINT [FK_BlogLinks_Blogs]
GO
/****** Object:  ForeignKey [FK_EntryComments_BlogEntries]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[EntryComments]  WITH CHECK ADD  CONSTRAINT [FK_EntryComments_BlogEntries] FOREIGN KEY([EntryId])
REFERENCES [dbo].[BlogEntries] ([EntryId])
GO
ALTER TABLE [dbo].[EntryComments] CHECK CONSTRAINT [FK_EntryComments_BlogEntries]
GO
/****** Object:  ForeignKey [FK_BlogEntryTags_BlogEntries]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogEntryTags]  WITH CHECK ADD  CONSTRAINT [FK_BlogEntryTags_BlogEntries] FOREIGN KEY([BlogEntryId])
REFERENCES [dbo].[BlogEntries] ([EntryId])
GO
ALTER TABLE [dbo].[BlogEntryTags] CHECK CONSTRAINT [FK_BlogEntryTags_BlogEntries]
GO
/****** Object:  ForeignKey [FK_BlogEntryTags_Tags]    Script Date: 09/15/2014 08:05:14 ******/
ALTER TABLE [dbo].[BlogEntryTags]  WITH CHECK ADD  CONSTRAINT [FK_BlogEntryTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([id])
GO
ALTER TABLE [dbo].[BlogEntryTags] CHECK CONSTRAINT [FK_BlogEntryTags_Tags]
GO
INSERT INTO [dbo].[Consumers] (ConsumerKey, ConsumerSecret, PublicKey, Name, ContactEmail, AutoGrant, AccessTokenLifetime) 
	VALUES ('708DB394-9CFD-4998-B3B4-4FF94830D505', 'EF4E7875-66F6-47CC-AFC9-7F49C5097ED1', '', 'AlwaysMoveForward Blog', 'artieac@gmail.com', 0, '7800')	
GO