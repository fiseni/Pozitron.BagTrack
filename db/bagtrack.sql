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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE TABLE [Airline] (
        [Id] int NOT NULL IDENTITY,
        [IATA] nvarchar(10) NOT NULL,
        [BagCode] nvarchar(10) NOT NULL,
        CONSTRAINT [PK_Airline] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE TABLE [Bag] (
        [Id] uniqueidentifier NOT NULL,
        [BagTagId] nvarchar(10) NOT NULL,
        [DeviceId] nvarchar(10) NOT NULL,
        [Carousel] nvarchar(10) NULL,
        [Flight] nvarchar(50) NULL,
        [Airline] nvarchar(10) NULL,
        [JulianDate] nvarchar(10) NULL,
        [Date] datetime2 NOT NULL,
        [IsResponseNeeded] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Bag] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE TABLE [Device] (
        [Id] nvarchar(10) NOT NULL,
        [Carousel] nvarchar(10) NOT NULL,
        CONSTRAINT [PK_Device] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE INDEX [IX_Bag_IsDeleted] ON [Bag] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE INDEX [IX_InboxMessage_IsDeleted] ON [InboxMessage] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    CREATE INDEX [IX_OutboxMessage_IsDeleted] ON [OutboxMessage] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230918201457_9e5eb58815bfa979ec1b')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230918201457_9e5eb58815bfa979ec1b', N'7.0.11');
END;
GO

COMMIT;
GO

