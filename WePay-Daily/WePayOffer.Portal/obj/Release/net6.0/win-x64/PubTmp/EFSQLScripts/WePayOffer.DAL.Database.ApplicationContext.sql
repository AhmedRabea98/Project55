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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    CREATE TABLE [Status] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    CREATE TABLE [ServiceNumber] (
        [Id] int NOT NULL IDENTITY,
        [Number] nvarchar(50) NOT NULL,
        [OfferId] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [StatusId] int NOT NULL,
        CONSTRAINT [PK_ServiceNumber] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceNumber_Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Status] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    CREATE TABLE [ServiceTransaction] (
        [Id] int NOT NULL IDENTITY,
        [ServiceNumberId] int NOT NULL,
        [IsSucceed] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [FunctionId] nvarchar(max) NOT NULL,
        [ResponseMessage] nvarchar(max) NOT NULL,
        [RequestId] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ServiceTransaction] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceTransaction_ServiceNumber_ServiceNumberId] FOREIGN KEY ([ServiceNumberId]) REFERENCES [ServiceNumber] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Status]'))
        SET IDENTITY_INSERT [Status] ON;
    EXEC(N'INSERT INTO [Status] ([Id], [Name])
    VALUES (1, N''New''),
    (2, N''Succeed''),
    (3, N''Failed''),
    (4, N''In Progress'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Status]'))
        SET IDENTITY_INSERT [Status] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    CREATE INDEX [IX_ServiceNumber_StatusId] ON [ServiceNumber] ([StatusId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    CREATE INDEX [IX_ServiceTransaction_ServiceNumberId] ON [ServiceTransaction] ([ServiceNumberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221101164357_initialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221101164357_initialMigration', N'6.0.9');
END;
GO

COMMIT;
GO

