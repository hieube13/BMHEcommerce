BEGIN TRANSACTION;
GO

ALTER TABLE [AppProducts] ADD [CategoryName] nvarchar(250) NULL;
GO

ALTER TABLE [AppProducts] ADD [CategorySlug] nvarchar(250) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240315085021_AddCategoryFieldInProduct', N'6.0.5');
GO

COMMIT;
GO

