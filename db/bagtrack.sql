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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230826165919_41eabab6431c4720c3ad')
BEGIN
    CREATE TABLE [Bag] (
        [Id] uniqueidentifier NOT NULL,
        [BagTrackId] nvarchar(10) NOT NULL,
        [DeviceId] nvarchar(10) NOT NULL,
        [IsResponseNeeded] nvarchar(1) NULL,
        [JulianDate] nvarchar(3) NULL,
        [IsDeleted] bit NOT NULL,
        [AuditCreatedTime] datetime2 NULL,
        [AuditCreatedByUserId] nvarchar(250) NULL,
        [AuditCreatedByUsername] nvarchar(250) NULL,
        [AuditModifiedTime] datetime2 NULL,
        [AuditModifiedByUserId] nvarchar(250) NULL,
        [AuditModifiedByUsername] nvarchar(250) NULL,
        CONSTRAINT [PK_Bag] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230826165919_41eabab6431c4720c3ad')
BEGIN
    CREATE INDEX [IX_Bag_IsDeleted] ON [Bag] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230826165919_41eabab6431c4720c3ad')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230826165919_41eabab6431c4720c3ad', N'7.0.10');
END;
GO

COMMIT;
GO

