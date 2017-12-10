
CREATE TABLE `EFMigrationsHistory` (

        `MigrationId` nvarchar(150) NOT NULL,

        `ProductVersion` nvarchar(32) NOT NULL,
		 PRIMARY KEY(`MigrationId`),
		UNIQUE INDEX `IX_MigrationHistory_Id` (`MigrationId` ASC)
    );


CREATE TABLE `ApiResources` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `Description` nvarchar(1000) NULL,

    `DisplayName` nvarchar(200) NULL,

    `Enabled` bit NOT NULL,

    `Name` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ApiResources_Id` (`Id` ASC)
);


CREATE TABLE `Clients` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `AbsoluteRefreshTokenLifetime` int NOT NULL,

    `AccessTokenLifetime` int NOT NULL,

    `AccessTokenType` int NOT NULL,

    `AllowAccessTokensViaBrowser` bit NOT NULL,

    `AllowOfflineAccess` bit NOT NULL,

    `AllowPlainTextPkce` bit NOT NULL,

    `AllowRememberConsent` bit NOT NULL,

    `AlwaysIncludeUserClaimsInIdToken` bit NOT NULL,

    `AlwaysSendClientClaims` bit NOT NULL,

    `AuthorizationCodeLifetime` int NOT NULL,

    `BackChannelLogoutSessionRequired` bit NOT NULL,

    `BackChannelLogoutUri` nvarchar(2000) NULL,

    `ClientClaimsPrefix` nvarchar(200) NULL,

    `ClientId` nvarchar(200) NOT NULL,

    `ClientName` nvarchar(200) NULL,

    `ClientUri` nvarchar(2000) NULL,

    `ConsentLifetime` int NULL,

    `Description` nvarchar(1000) NULL,

    `EnableLocalLogin` bit NOT NULL,

    `Enabled` bit NOT NULL,

    `FrontChannelLogoutSessionRequired` bit NOT NULL,

    `FrontChannelLogoutUri` nvarchar(2000) NULL,

    `IdentityTokenLifetime` int NOT NULL,

    `IncludeJwtId` bit NOT NULL,

    `LogoUri` nvarchar(2000) NULL,

    `PairWiseSubjectSalt` nvarchar(200) NULL,

    `ProtocolType` nvarchar(200) NOT NULL,

    `RefreshTokenExpiration` int NOT NULL,

    `RefreshTokenUsage` int NOT NULL,

    `RequireClientSecret` bit NOT NULL,

    `RequireConsent` bit NOT NULL,

    `RequirePkce` bit NOT NULL,

    `SlidingRefreshTokenLifetime` int NOT NULL,

    `UpdateAccessTokenClaimsOnRefresh` bit NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_Clients_Id` (`Id` ASC)
);


CREATE TABLE `IdentityResources` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `Description` nvarchar(1000) NULL,

    `DisplayName` nvarchar(200) NULL,

    `Emphasize` bit NOT NULL,

    `Enabled` bit NOT NULL,

    `Name` nvarchar(200) NOT NULL,

    `Required` bit NOT NULL,

    `ShowInDiscoveryDocument` bit NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_IdentityResources_Id` (`Id` ASC)
);


CREATE TABLE `ApiClaims` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ApiResourceId` int NOT NULL,

    `Type` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ApiClaims_Id` (`Id` ASC),
    CONSTRAINT `FK_ApiClaims_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE

);



CREATE TABLE `ApiScopes` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ApiResourceId` int NOT NULL,

    `Description` nvarchar(1000) NULL,

    `DisplayName` nvarchar(200) NULL,

    `Emphasize` bit NOT NULL,

    `Name` nvarchar(200) NOT NULL,

    `Required` bit NOT NULL,

    `ShowInDiscoveryDocument` bit NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ApiScopes_Id` (`Id` ASC),
    CONSTRAINT `FK_ApiScopes_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ApiSecrets` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ApiResourceId` int NOT NULL,

    `Description` nvarchar(1000) NULL,

    `Expiration` datetime NULL,

    `Type` nvarchar(250) NULL,

    `Value` nvarchar(2000) NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ApiSecrets_Id` (`Id` ASC),
    CONSTRAINT `FK_ApiSecrets_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientClaims` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Type` nvarchar(250) NOT NULL,

    `Value` nvarchar(250) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientClaims_Id` (`Id` ASC),
    CONSTRAINT `FK_ClientClaims_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);

CREATE TABLE `ClientCorsOrigins` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Origin` nvarchar(150) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientCorsOrigins_Id` (`Id` ASC),
    CONSTRAINT `FK_ClientCorsOrigins_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientGrantTypes` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `GrantType` nvarchar(250) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientGrantTypes_Id` (`Id` ASC),
    CONSTRAINT `FK_ClientGrantTypes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientIdPRestrictions` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Provider` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientIdPRestrictions_Id` (`Id` ASC),
    CONSTRAINT `FK_ClientIdPRestrictions_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientPostLogoutRedirectUris` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `PostLogoutRedirectUri` nvarchar(2000) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientPostLogoutRedirectUris_Id` (`Id` ASC),

    CONSTRAINT `FK_ClientPostLogoutRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientProperties` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Key` nvarchar(250) NOT NULL,

    `Value` nvarchar(2000) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientProperties_Id` (`Id` ASC),

    CONSTRAINT `FK_ClientProperties_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientRedirectUris` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `RedirectUri` nvarchar(2000) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientRedirectUris_Id` (`Id` ASC),

    CONSTRAINT `FK_ClientRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientScopes` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Scope` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientScopes_Id` (`Id` ASC),

    CONSTRAINT `FK_ClientScopes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ClientSecrets` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ClientId` int NOT NULL,

    `Description` nvarchar(2000) NULL,

    `Expiration` datetime NULL,

    `Type` nvarchar(250) NULL,

    `Value` nvarchar(2000) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ClientSecrets_Id` (`Id` ASC),

    CONSTRAINT `FK_ClientSecrets_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `IdentityClaims` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `IdentityResourceId` int NOT NULL,

    `Type` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_IdentityClaims_Id` (`Id` ASC),

    CONSTRAINT `FK_IdentityClaims_IdentityResources_IdentityResourceId` FOREIGN KEY (`IdentityResourceId`) REFERENCES `IdentityResources` (`Id`) ON DELETE CASCADE

);


CREATE TABLE `ApiScopeClaims` (

    `Id` int NOT NULL AUTO_INCREMENT,

    `ApiScopeId` int NOT NULL,

    `Type` nvarchar(200) NOT NULL,

    PRIMARY KEY(`Id`),
	UNIQUE INDEX `IX_ApiScopeClaims_Id` (`Id` ASC),
    CONSTRAINT `FK_ApiScopeClaims_ApiScopes_ApiScopeId` FOREIGN KEY (`ApiScopeId`) REFERENCES `ApiScopes` (`Id`) ON DELETE CASCADE

);


CREATE INDEX `IX_ApiClaims_ApiResourceId` ON `ApiClaims` (`ApiResourceId`);


CREATE UNIQUE INDEX `IX_ApiResources_Name` ON `ApiResources` (`Name`);


CREATE INDEX `IX_ApiScopeClaims_ApiScopeId` ON `ApiScopeClaims` (`ApiScopeId`);


CREATE INDEX `IX_ApiScopes_ApiResourceId` ON `ApiScopes` (`ApiResourceId`);


CREATE UNIQUE INDEX `IX_ApiScopes_Name` ON `ApiScopes` (`Name`);


CREATE INDEX `IX_ApiSecrets_ApiResourceId` ON `ApiSecrets` (`ApiResourceId`);


CREATE INDEX `IX_ClientClaims_ClientId` ON `ClientClaims` (`ClientId`);


CREATE INDEX `IX_ClientCorsOrigins_ClientId` ON `ClientCorsOrigins` (`ClientId`);


CREATE INDEX `IX_ClientGrantTypes_ClientId` ON `ClientGrantTypes` (`ClientId`);


CREATE INDEX `IX_ClientIdPRestrictions_ClientId` ON `ClientIdPRestrictions` (`ClientId`);


CREATE INDEX `IX_ClientPostLogoutRedirectUris_ClientId` ON `ClientPostLogoutRedirectUris` (`ClientId`);


CREATE INDEX `IX_ClientProperties_ClientId` ON `ClientProperties` (`ClientId`);


CREATE INDEX `IX_ClientRedirectUris_ClientId` ON `ClientRedirectUris` (`ClientId`);


CREATE UNIQUE INDEX `IX_Clients_ClientId` ON `Clients` (`ClientId`);


CREATE INDEX `IX_ClientScopes_ClientId` ON `ClientScopes` (`ClientId`);


CREATE INDEX `IX_ClientSecrets_ClientId` ON `ClientSecrets` (`ClientId`);


CREATE INDEX `IX_IdentityClaims_IdentityResourceId` ON `IdentityClaims` (`IdentityResourceId`);


CREATE UNIQUE INDEX `IX_IdentityResources_Name` ON `IdentityResources` (`Name`);


INSERT INTO `EFMigrationsHistory` (`MigrationId`, `ProductVersion`)

VALUES (N'20170927170433_Config', N'2.0.0-rtm-26452');

