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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230915144924_02810a5b6227d7fa55c6')
BEGIN
    CREATE TABLE [Bag] (
        [Id] uniqueidentifier NOT NULL,
        [BagTrackId] nvarchar(10) NOT NULL,
        [DeviceId] nvarchar(10) NOT NULL,
        [Carousel] nvarchar(10) NULL,
        [Flight] nvarchar(50) NULL,
        [Airline] nvarchar(250) NULL,
        [JulianDate] nvarchar(10) NULL,
        [Date] datetime2 NOT NULL,
        [IsResponseNeeded] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Bag] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230915144924_02810a5b6227d7fa55c6')
BEGIN
    CREATE TABLE [Device] (
        [Id] nvarchar(10) NOT NULL,
        [Carousel] nvarchar(250) NOT NULL,
        CONSTRAINT [PK_Device] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230915144924_02810a5b6227d7fa55c6')
BEGIN
    CREATE INDEX [IX_Bag_IsDeleted] ON [Bag] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230915144924_02810a5b6227d7fa55c6')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230915144924_02810a5b6227d7fa55c6', N'7.0.10');
END;
GO

COMMIT;
GO

