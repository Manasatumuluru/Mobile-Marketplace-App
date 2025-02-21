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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    CREATE TABLE [Contact] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Contact] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    CREATE TABLE [Courses] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Caption] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [MobileNumber] nvarchar(max) NOT NULL,
        [Bio] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    CREATE TABLE [Enrollments] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [CourseId] int NOT NULL,
        CONSTRAINT [PK_Enrollments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Enrollments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    CREATE INDEX [IX_Enrollments_CourseId] ON [Enrollments] ([CourseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241212181823_mssql.onprem_migration_871'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241212181823_mssql.onprem_migration_871', N'9.0.0');
END;

COMMIT;
GO

