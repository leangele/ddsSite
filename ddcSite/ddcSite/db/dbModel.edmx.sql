
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2016 08:59:06
-- Generated from EDMX file: C:\Users\jeffry tobon\documents\visual studio 2015\Projects\ddcSite\ddcSite\db\dbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ddc];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailOrder_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailOrder] DROP CONSTRAINT [FK_DetailOrder_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailOrder_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailOrder] DROP CONSTRAINT [FK_DetailOrder_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Clients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Orders_Clients];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceXDetailsOrder_DetailOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceXDetailsOrder] DROP CONSTRAINT [FK_ServiceXDetailsOrder_DetailOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceXDetailsOrder_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceXDetailsOrder] DROP CONSTRAINT [FK_ServiceXDetailsOrder_Service];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachment];
GO
IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[Configuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configuration];
GO
IF OBJECT_ID(N'[dbo].[DetailOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailOrder];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[ServiceXDetailsOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceXDetailsOrder];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [NameUpdated] varchar(500)  NULL,
    [SizeFile] decimal(8,4)  NULL,
    [IdOrder] decimal(18,0)  NULL,
    [Format] varchar(3)  NULL,
    [kindFile] varchar(50)  NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Name] varchar(200)  NOT NULL,
    [Address] varchar(500)  NOT NULL,
    [City] varchar(50)  NULL,
    [State] varchar(2)  NULL,
    [PostalCode] varchar(5)  NULL,
    [Phone] varchar(15)  NULL,
    [IsAvailable] bit  NOT NULL,
    [licenseNumber] varchar(50)  NULL
);
GO

-- Creating table 'Configurations'
CREATE TABLE [dbo].[Configurations] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [NameConfig] varchar(50)  NOT NULL,
    [ValueConfig] varchar(max)  NULL,
    [ValueXML] nvarchar(max)  NULL
);
GO

-- Creating table 'DetailOrders'
CREATE TABLE [dbo].[DetailOrders] (
    [ID] decimal(18,0)  NOT NULL,
    [IdOrder] decimal(18,0)  NULL,
    [Teeth] varchar(1000)  NULL,
    [BuccalMargin] varchar(50)  NULL,
    [LingualMargin] varchar(50)  NULL,
    [BuccalTissue] varchar(50)  NULL,
    [Compression] varchar(50)  NULL,
    [AbutmentsParallel] varchar(50)  NULL,
    [Note] varchar(max)  NULL,
    [IdProduct] decimal(18,0)  NULL,
    [Contact] varchar(50)  NULL,
    [Oclussion] varchar(50)  NULL,
    [Material] varchar(50)  NULL,
    [Shade] varchar(50)  NULL,
    [DesignRequirements] varchar(50)  NULL,
    [ModelServices] varchar(50)  NULL,
    [ArticulatorType] varchar(50)  NULL,
    [ModelImplantConnection] varchar(50)  NULL,
    [Amount] int  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Name] varchar(200)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [DateDelivery] datetime  NOT NULL,
    [Location] varchar(500)  NULL,
    [Score] decimal(4,2)  NOT NULL,
    [DateCreation] nchar(10)  NOT NULL,
    [IdClient] decimal(18,0)  NOT NULL,
    [PatientName] varchar(50)  NOT NULL,
    [PatientLastName] varchar(50)  NOT NULL,
    [Coupon] varchar(10)  NULL,
    [Gender] varchar(1)  NULL,
    [Age] int  NULL,
    [PriceTax] decimal(19,4)  NULL,
    [PriceTotal] decimal(19,4)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Description] varchar(max)  NULL,
    [InLabWorkingDays] int  NULL,
    [Price] decimal(19,4)  NULL,
    [Notes] varchar(max)  NULL,
    [IsAvailable] bit  NOT NULL,
    [IsDigital] bit  NULL,
    [PriceEach] decimal(19,4)  NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [IsOptional] bit  NOT NULL,
    [Description] varchar(max)  NULL,
    [Price] decimal(19,4)  NULL,
    [Notes] varchar(max)  NULL,
    [IsAvailable] bit  NOT NULL
);
GO

-- Creating table 'ServiceXDetailsOrders'
CREATE TABLE [dbo].[ServiceXDetailsOrders] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [IdDetailOrder] decimal(18,0)  NOT NULL,
    [Idservice] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [idClient] decimal(18,0)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [PK_Attachments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [PK_Configurations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'DetailOrders'
ALTER TABLE [dbo].[DetailOrders]
ADD CONSTRAINT [PK_DetailOrders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ServiceXDetailsOrders'
ALTER TABLE [dbo].[ServiceXDetailsOrders]
ADD CONSTRAINT [PK_ServiceXDetailsOrders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdClient] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Clients]
    FOREIGN KEY ([IdClient])
    REFERENCES [dbo].[Clients]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Clients'
CREATE INDEX [IX_FK_Orders_Clients]
ON [dbo].[Orders]
    ([IdClient]);
GO

-- Creating foreign key on [IdOrder] in table 'DetailOrders'
ALTER TABLE [dbo].[DetailOrders]
ADD CONSTRAINT [FK_DetailOrder_Order]
    FOREIGN KEY ([IdOrder])
    REFERENCES [dbo].[Orders]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailOrder_Order'
CREATE INDEX [IX_FK_DetailOrder_Order]
ON [dbo].[DetailOrders]
    ([IdOrder]);
GO

-- Creating foreign key on [IdProduct] in table 'DetailOrders'
ALTER TABLE [dbo].[DetailOrders]
ADD CONSTRAINT [FK_DetailOrder_Product]
    FOREIGN KEY ([IdProduct])
    REFERENCES [dbo].[Products]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailOrder_Product'
CREATE INDEX [IX_FK_DetailOrder_Product]
ON [dbo].[DetailOrders]
    ([IdProduct]);
GO

-- Creating foreign key on [IdDetailOrder] in table 'ServiceXDetailsOrders'
ALTER TABLE [dbo].[ServiceXDetailsOrders]
ADD CONSTRAINT [FK_ServiceXDetailsOrder_DetailOrder]
    FOREIGN KEY ([IdDetailOrder])
    REFERENCES [dbo].[DetailOrders]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceXDetailsOrder_DetailOrder'
CREATE INDEX [IX_FK_ServiceXDetailsOrder_DetailOrder]
ON [dbo].[ServiceXDetailsOrders]
    ([IdDetailOrder]);
GO

-- Creating foreign key on [Idservice] in table 'ServiceXDetailsOrders'
ALTER TABLE [dbo].[ServiceXDetailsOrders]
ADD CONSTRAINT [FK_ServiceXDetailsOrder_Service]
    FOREIGN KEY ([Idservice])
    REFERENCES [dbo].[Services]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceXDetailsOrder_Service'
CREATE INDEX [IX_FK_ServiceXDetailsOrder_Service]
ON [dbo].[ServiceXDetailsOrders]
    ([Idservice]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------