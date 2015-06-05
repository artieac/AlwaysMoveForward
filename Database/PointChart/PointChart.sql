CREATE TABLE `Users`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`IsSiteAdministrator` bit NOT NULL,
	`AccessToken` nvarchar(36) NULL,
	`AccessTokenSecret` nvarchar(36) NULL,
	`OAuthServiceUserId` bigint NULL,
	`FirstName` nvarchar(50) NULL,
	`LastName` nvarchar(50) NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_Users_Id` (`Id` ASC),
	INDEX `IX_Users_OAuthServiceId` (`OAuthServiceUserId` ASC));

CREATE TABLE `Tasks`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`Name` nvarchar(255) NOT NULL,
	`Points` float NOT NULL,
	`MaxAllowedDaily` int NOT NULL,
	`CreatorId` bigint NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_Tasks_Id` (`Id` ASC),
	INDEX `IX_Tasks_CreatorId` (`CreatorId` ASC));

CREATE TABLE `PointsSpent`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`PointEarnerId` bigint NOT NULL,
	`Description` nvarchar(255) NOT NULL,
	`DateSpent` datetime NOT NULL,
	`Amount` float NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_PointsSpent_Id` (`Id` ASC),
	INDEX `IX_PointsSpent_PointEarnerId` (`PointEarnerId` ASC));

CREATE TABLE `PointEarners`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`UserId` bigint NOT NULL,
	`PointEarnerId` bigint NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_PointEarners_Id` (`Id` ASC),
	INDEX `IX_PointEarners_UserId` (`UserId` ASC));

CREATE TABLE `CompletedTasks`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`TaskId` bigint NOT NULL,
	`DateCompleted` datetime NOT NULL,
	`NumberOfTimesCompleted` int NOT NULL,
	`ChartId` bigint NOT NULL,
	`PointValue` float NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_CompletedTasks_Id` (`Id` ASC),
	INDEX `IX_CompletedTasks_ChartId` (`ChartId` ASC));

CREATE TABLE `ChartTasks`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`ChartId` bigint NOT NULL,
	`TaskId` bigint NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_ChartTasks_Id` (`Id` ASC),
	INDEX `IX_ChartTasks_ChartId` (`ChartId` ASC));

CREATE TABLE `Charts`(
	`Id` BIGINT NOT NULL AUTO_INCREMENT,
	`CreatorId` bigint NOT NULL,
	`PointEarnerId` bigint NOT NULL,
	`Name` nvarchar(100) NOT NULL,
	PRIMARY KEY (`Id`),
	UNIQUE INDEX `IX_Charts_Id` (`Id` ASC),
	INDEX `IX_Charts_CreatorId` (`CreatorId` ASC),
	INDEX `IX_Charts_PointEarnerId` (`PointEarnerId` ASC));
