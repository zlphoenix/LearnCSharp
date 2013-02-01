
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/16/2013 14:39:02
-- Generated from EDMX file: D:\Doc\Dropbox\Work\Code\CSharpFeatures\CSharpFeatures\Data\MyDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AppDemo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Sue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sue];
GO
IF OBJECT_ID(N'[dbo].[SueType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SueType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Sues'
CREATE TABLE [dbo].[Sues] (
    [ID] uniqueidentifier  NOT NULL,
    [SysVersion] int  NOT NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] nvarchar(max)  NULL,
    [UpdatedOn] datetime  NULL,
    [UpdatedBy] nvarchar(max)  NULL,
    [SueCode] nvarchar(max)  NOT NULL,
    [EnforceOrgan] nvarchar(max)  NULL,
    [RegisterDate] datetime  NULL,
    [DelayApplyDate] datetime  NULL,
    [IsEnd] nvarchar(max)  NULL,
    [ConductResult] nvarchar(max)  NULL,
    [EndDate] datetime  NULL,
    [PunishOpion] nvarchar(max)  NULL,
    [SueName] nvarchar(max)  NULL,
    [SueLevel] nvarchar(max)  NULL,
    [SuePosition] nvarchar(max)  NULL,
    [SueBigType] nvarchar(max)  NULL,
    [SueSmallType] nvarchar(max)  NULL,
    [SueSource] nvarchar(max)  NULL,
    [SueContent] nvarchar(max)  NULL,
    [SueReason] nvarchar(max)  NULL,
    [Reporter] nvarchar(max)  NULL,
    [ReportDate] datetime  NULL,
    [EnforcePersons] nvarchar(max)  NULL,
    [PartyName] nvarchar(max)  NULL,
    [PartyGender] nvarchar(max)  NULL,
    [PartyAge] int  NULL,
    [PartyVocation] nvarchar(max)  NULL,
    [PartyTel] nvarchar(max)  NULL,
    [PartyCorporation] nvarchar(max)  NULL,
    [PartyPost] nvarchar(max)  NULL,
    [PartyAddr] nvarchar(max)  NULL,
    [RegisterCode] nvarchar(max)  NULL,
    [SuePunish] nvarchar(max)  NULL,
    [SueFine] decimal(10,3)  NULL,
    [ConductDate] datetime  NULL,
    [HotlineCaseCode] nvarchar(max)  NULL,
    [Longitude] nvarchar(max)  NULL,
    [Latitude] nvarchar(max)  NULL,
    [CoordinateType] int  NULL,
    [IsDelay] bit  NULL,
    [DelayDays] datetime  NULL,
    [DelayApplyLoginName] nvarchar(max)  NULL,
    [IsDelayEnd] bit  NULL,
    [DecisionCode] nvarchar(max)  NULL,
    [ReceiptCode] nvarchar(max)  NULL,
    [PunishType] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL,
    [SueType_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'SueTypes'
CREATE TABLE [dbo].[SueTypes] (
    [ID] uniqueidentifier  NOT NULL,
    [SysVersion] int  NOT NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] nvarchar(max)  NULL,
    [UpdatedOn] datetime  NULL,
    [UpdatedBy] nvarchar(max)  NULL,
    [Code] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Pid] nvarchar(max)  NULL,
    [isBigType] bit  NULL,
    [SueTypeIndex] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Sues'
ALTER TABLE [dbo].[Sues]
ADD CONSTRAINT [PK_Sues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SueTypes'
ALTER TABLE [dbo].[SueTypes]
ADD CONSTRAINT [PK_SueTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SueType_ID] in table 'Sues'
ALTER TABLE [dbo].[Sues]
ADD CONSTRAINT [FK_SueTypeSue]
    FOREIGN KEY ([SueType_ID])
    REFERENCES [dbo].[SueTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SueTypeSue'
CREATE INDEX [IX_FK_SueTypeSue]
ON [dbo].[Sues]
    ([SueType_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------