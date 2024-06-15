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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [discount] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(50) NOT NULL,
        [desc] varchar(300) NULL,
        [discount_percent] decimal(18,0) NOT NULL,
        [active] bit NOT NULL,
        [created_at] rowversion NOT NULL,
        [modified_at] datetime NULL DEFAULT ((getdate())),
        [delete_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_discount] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [payment_details] (
        [id] int NOT NULL IDENTITY,
        [amount] int NOT NULL,
        [provider] varchar(50) NULL,
        [status] varchar(50) NULL,
        [created_at] datetime NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_payment_details] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [product_category] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(50) NOT NULL,
        [desc] varchar(300) NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        [delete_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_product_category] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [product_inventory] (
        [id] int NOT NULL IDENTITY,
        [quantity] int NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        [delete_at] datetime NULL,
        CONSTRAINT [PK_product_inventory] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [user] (
        [id] int NOT NULL IDENTITY,
        [username] varchar(50) NOT NULL,
        [password] varchar(50) NOT NULL,
        [first_name] varchar(50) NULL,
        [last_name] varchar(50) NULL,
        [telephone] varchar(50) NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NOT NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_NewTable] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [order_details] (
        [id] int NOT NULL IDENTITY,
        [user_id] int NOT NULL,
        [total] decimal(18,0) NOT NULL,
        [payment_id] int NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_order_details] PRIMARY KEY ([id]),
        CONSTRAINT [FK_order_details_payment_details] FOREIGN KEY ([payment_id]) REFERENCES [payment_details] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [product] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(50) NOT NULL,
        [desc] varchar(300) NULL,
        [SKU] nvarchar(100) NOT NULL,
        [category_id] int NOT NULL,
        [inventory_id] int NOT NULL,
        [price] decimal(18,0) NOT NULL,
        [discount_id] int NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        [deleted_at] datetime NULL DEFAULT ((getdate())),
        [image] varchar(200) NULL,
        CONSTRAINT [PK_product] PRIMARY KEY ([id]),
        CONSTRAINT [FK_product_discount] FOREIGN KEY ([discount_id]) REFERENCES [discount] ([id]),
        CONSTRAINT [FK_product_product_category] FOREIGN KEY ([category_id]) REFERENCES [product_category] ([id]),
        CONSTRAINT [FK_product_product_inventory] FOREIGN KEY ([inventory_id]) REFERENCES [product_inventory] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [shopping_session] (
        [id] int NOT NULL IDENTITY,
        [user_id] int NOT NULL,
        [total] decimal(18,0) NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_shopping_session] PRIMARY KEY ([id]),
        CONSTRAINT [FK_shopping_session_user] FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [user_address] (
        [id] int NOT NULL IDENTITY,
        [user_id] int NOT NULL,
        [address_line1] varchar(100) NOT NULL,
        [address_line2] varchar(100) NULL,
        [city] varchar(100) NOT NULL,
        [postal_code] varchar(10) NULL,
        [country] varchar(50) NULL,
        [telephone] varchar(11) NOT NULL,
        [mobile] varchar(50) NOT NULL,
        CONSTRAINT [PK_user_address] PRIMARY KEY ([id]),
        CONSTRAINT [FK_user_address_user] FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [user_payment] (
        [id] int NOT NULL IDENTITY,
        [user_id] int NOT NULL,
        [payment_type] varchar(50) NOT NULL,
        [provider] varchar(50) NOT NULL,
        [account_no] int NOT NULL,
        [expiry] datetime NOT NULL,
        CONSTRAINT [PK_user_payment] PRIMARY KEY ([id]),
        CONSTRAINT [FK_user_payment_user] FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [order_items] (
        [id] int NOT NULL IDENTITY,
        [order_id] int NOT NULL,
        [product_id] int NOT NULL,
        [quantity] int NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_order_items] PRIMARY KEY ([id]),
        CONSTRAINT [FK_order_items_order_details] FOREIGN KEY ([order_id]) REFERENCES [order_details] ([id]),
        CONSTRAINT [FK_order_items_product] FOREIGN KEY ([product_id]) REFERENCES [product] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE TABLE [cart_item] (
        [id] int NOT NULL IDENTITY,
        [session_id] int NOT NULL,
        [product_id] int NOT NULL,
        [quantity] int NOT NULL,
        [created_at] datetime NOT NULL DEFAULT ((getdate())),
        [modified_at] datetime NULL DEFAULT ((getdate())),
        CONSTRAINT [PK_cart_item] PRIMARY KEY ([id]),
        CONSTRAINT [FK_cart_item_product] FOREIGN KEY ([product_id]) REFERENCES [product] ([id]),
        CONSTRAINT [FK_cart_item_shopping_session] FOREIGN KEY ([session_id]) REFERENCES [shopping_session] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_cart_item_session_id] ON [cart_item] ([session_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_cart_item_product_id] ON [cart_item] ([product_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_order_details_payment_id] ON [order_details] ([payment_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_order_items_order_id] ON [order_items] ([order_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_order_items_product_id] ON [order_items] ([product_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_product_discount_id] ON [product] ([discount_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_product_category_id] ON [product] ([category_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_product_inventory_id] ON [product] ([inventory_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_shopping_session_user_id] ON [shopping_session] ([user_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_user_address_user_id] ON [user_address] ([user_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    CREATE INDEX [IX_user_payment_user_id] ON [user_payment] ([user_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503151943_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240503151943_Initial', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240504024816_Seeding data for category product'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'desc', N'name') AND [object_id] = OBJECT_ID(N'[product_category]'))
        SET IDENTITY_INSERT [product_category] ON;
    EXEC(N'INSERT INTO [product_category] ([id], [desc], [name])
    VALUES (1, ''Phụ kiện unisex'', ''Accessories''),
    (2, '''', ''Fitness''),
    (3, ''Quần áo unisex'', ''Clothing''),
    (4, ''Thiết bị điện tử'', ''Electronics'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'desc', N'name') AND [object_id] = OBJECT_ID(N'[product_category]'))
        SET IDENTITY_INSERT [product_category] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240504024816_Seeding data for category product'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240504024816_Seeding data for category product', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240504031156_Seeding data for discount'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'active', N'desc', N'discount_percent', N'name') AND [object_id] = OBJECT_ID(N'[discount]'))
        SET IDENTITY_INSERT [discount] ON;
    EXEC(N'INSERT INTO [discount] ([id], [active], [desc], [discount_percent], [name])
    VALUES (1, CAST(0 AS bit), ''Chương trình giảm giá'', 20.0, ''Giảm 20%''),
    (2, CAST(0 AS bit), ''Chương trình giảm giá các ngày lễ trong năm'', 25.0, ''Giảm 25%''),
    (3, CAST(0 AS bit), ''Hàng tồn kho'', 50.0, ''Giảm 50%'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'id', N'active', N'desc', N'discount_percent', N'name') AND [object_id] = OBJECT_ID(N'[discount]'))
        SET IDENTITY_INSERT [discount] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240504031156_Seeding data for discount'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240504031156_Seeding data for discount', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [order_details] DROP CONSTRAINT [FK_order_details_payment_details];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [product] DROP CONSTRAINT [FK_product_product_inventory];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [shopping_session] DROP CONSTRAINT [FK_shopping_session_user];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [cart_item] ADD CONSTRAINT [FK_cart_item_product] FOREIGN KEY ([product_id]) REFERENCES [product] ([id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [order_details] ADD CONSTRAINT [FK_order_details_payment_details] FOREIGN KEY ([payment_id]) REFERENCES [payment_details] ([id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [product] ADD CONSTRAINT [FK_product_product_inventory] FOREIGN KEY ([inventory_id]) REFERENCES [product_inventory] ([id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    ALTER TABLE [shopping_session] ADD CONSTRAINT [FK_shopping_session_user] FOREIGN KEY ([user_id]) REFERENCES [user] ([id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145535_Update delete cascade'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240506145535_Update delete cascade', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240506145817_Update Delete ON Cascade'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240506145817_Update Delete ON Cascade', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240508081751_Adding Image Table'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [FileDescription] nvarchar(max) NULL,
        [FileExtension] nvarchar(max) NOT NULL,
        [FileSizeInBytes] bigint NOT NULL,
        [FilePath] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240508081751_Adding Image Table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240508081751_Adding Image Table', N'8.0.4');
END;
GO

COMMIT;
GO

