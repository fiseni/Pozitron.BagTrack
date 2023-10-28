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

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    DROP INDEX [IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted] ON [Flight];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    ALTER TABLE [Flight] ADD [Agent] varchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    ALTER TABLE [Flight] ADD [FirstBag] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    ALTER TABLE [Flight] ADD [LastBag] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    CREATE INDEX [IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted] ON [Flight] ([AirlineIATA], [Number], [OriginDate], [IsDeleted]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231006081920_2eaea6109706c478ac99')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231006081920_2eaea6109706c478ac99', N'7.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_OutboxMessage_IsDeleted] ON [OutboxMessage];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_InboxMessage_IsDeleted] ON [InboxMessage];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_Flight_AirlineIATA_ActiveCarousel_IsDeleted_Start_Stop] ON [Flight];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_Flight_AirlineIATA_Number_OriginDate_IsDeleted] ON [Flight];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_Flight_IsDeleted] ON [Flight];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DROP INDEX [IX_Bag_IsDeleted] ON [Bag];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OutboxMessage]') AND [c].[name] = N'IsDeleted');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [OutboxMessage] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [OutboxMessage] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InboxMessage]') AND [c].[name] = N'IsDeleted');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [InboxMessage] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [InboxMessage] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Flight]') AND [c].[name] = N'IsDeleted');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Flight] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Flight] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bag]') AND [c].[name] = N'IsDeleted');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Bag] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Bag] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104045_bdb7e8b26614519ec58d')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231028104045_bdb7e8b26614519ec58d', N'7.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104645_19da469a55e95d282d82')
BEGIN
    ALTER TABLE [Bag] ADD [Agent] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028104645_19da469a55e95d282d82')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231028104645_19da469a55e95d282d82', N'7.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028160850_c98f977fecd46df95cbf')
BEGIN
    CREATE INDEX [IX_Flight_AirlineIATA_ActiveCarousel_Start_Stop] ON [Flight] ([AirlineIATA], [ActiveCarousel], [Start], [Stop]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028160850_c98f977fecd46df95cbf')
BEGIN
    CREATE INDEX [IX_Flight_AirlineIATA_Number_OriginDate] ON [Flight] ([AirlineIATA], [Number], [OriginDate]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028160850_c98f977fecd46df95cbf')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231028160850_c98f977fecd46df95cbf', N'7.0.11');
END;
GO

COMMIT;
GO

