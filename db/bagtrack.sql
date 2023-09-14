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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230914230936_38d677a10d88e731ddde')
BEGIN
    CREATE TABLE [Bag] (
        [Id] uniqueidentifier NOT NULL,
        [BagTrackId] nvarchar(10) NOT NULL,
        [DeviceId] nvarchar(10) NOT NULL,
        [IsResponseNeeded] bit NOT NULL,
        [JulianDate] nvarchar(10) NULL,
        [IsDeleted] bit NOT NULL,
        [AuditCreatedAt] datetime2 NULL,
        [AuditCreatedBy] nvarchar(250) NULL,
        [AuditCreatedByUsername] nvarchar(250) NULL,
        [AuditModifiedAt] datetime2 NULL,
        [AuditModifiedBy] nvarchar(250) NULL,
        [AuditModifiedByUsername] nvarchar(250) NULL,
        CONSTRAINT [PK_Bag] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230914230936_38d677a10d88e731ddde')
BEGIN
    CREATE INDEX [IX_Bag_IsDeleted] ON [Bag] ([IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230914230936_38d677a10d88e731ddde')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230914230936_38d677a10d88e731ddde', N'7.0.10');
END;
GO

COMMIT;
GO

