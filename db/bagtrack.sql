IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [Airline] (
        [Id] int NOT NULL IDENTITY,
        [IATA] varchar(10) NOT NULL,
        [BagCode] varchar(10) NOT NULL,
        CONSTRAINT [PK_Airline] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [Bag] (
        [Id] uniqueidentifier NOT NULL,
        [BagTagId] varchar(10) NOT NULL,
        [DeviceId] varchar(10) NOT NULL,
        [Carousel] varchar(10) NULL,
        [AirlineIATA] varchar(10) NULL,
        [Flight] varchar(50) NULL,
        [JulianDate] varchar(10) NULL,
        [Date] datetime2 NOT NULL,
        [IsResponseNeeded] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Bag] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [Device] (
        [Id] varchar(10) NOT NULL,
        [Carousel] varchar(10) NOT NULL,
        CONSTRAINT [PK_Device] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [Flight] (
        [Id] uniqueidentifier NOT NULL,
        [AirlineIATA] varchar(10) NOT NULL,
        [Number] varchar(10) NOT NULL,
        [OriginDate] datetime2 NOT NULL,
        [ActiveCarousel] varchar(10) NULL,
        [AllocatedCarousel] varchar(10) NULL,
        [Start] datetime2 NULL,
        [Stop] datetime2 NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Flight] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [InboxMessage] (
        [Id] uniqueidentifier NOT NULL,
        [Type] int NOT NULL,
        [Status] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ProcessedDate] datetime2 NULL,
        [Data] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_InboxMessage] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE TABLE [OutboxMessage] (
        [Id] uniqueidentifier NOT NULL,
        [Type] int NOT NULL,
        [Status] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ProcessedDate] datetime2 NULL,
        [Data] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_OutboxMessage] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Bag_BagTagId] ON [Bag] ([BagTagId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Bag_Date] ON [Bag] ([Date]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Bag_IsDeleted] ON [Bag] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Flight_AirlineIATA_ActiveCarousel_IsDeleted_Start_Stop] ON [Flight] ([AirlineIATA], [ActiveCarousel], [IsDeleted], [Start], [Stop]) INCLUDE ([Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted] ON [Flight] ([AirlineIATA], [Number], [OriginDate], [IsDeleted]) INCLUDE ([ActiveCarousel], [AllocatedCarousel], [Start], [Stop]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_Flight_IsDeleted] ON [Flight] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_InboxMessage_IsDeleted] ON [InboxMessage] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    CREATE INDEX [IX_OutboxMessage_IsDeleted] ON [OutboxMessage] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230925131223_45573a740db7ae0601d2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230925131223_45573a740db7ae0601d2', N'7.0.11');
END;
GO

COMMIT;
GO

