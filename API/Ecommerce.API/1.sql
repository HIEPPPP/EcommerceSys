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
GO

CREATE TABLE [payment_details] (
    [id] int NOT NULL IDENTITY,
    [amount] int NOT NULL,
    [provider] varchar(50) NULL,
    [status] varchar(50) NULL,
    [created_at] datetime NULL DEFAULT ((getdate())),
    [modified_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK_payment_details] PRIMARY KEY ([id])
);
GO

CREATE TABLE [product_category] (
    [id] int NOT NULL IDENTITY,
    [name] varchar(50) NOT NULL,
    [desc] varchar(300) NOT NULL,
    [created_at] datetime NOT NULL DEFAULT ((getdate())),
    [modified_at] datetime NULL DEFAULT ((getdate())),
    [delete_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK_product_category] PRIMARY KEY ([id])
);
GO

CREATE TABLE [product_inventory] (
    [id] int NOT NULL IDENTITY,
    [quantity] int NOT NULL,
    [created_at] datetime NOT NULL DEFAULT ((getdate())),
    [modified_at] datetime NULL DEFAULT ((getdate())),
    [delete_at] datetime NULL,
    CONSTRAINT [PK_product_inventory] PRIMARY KEY ([id])
);
GO

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
GO

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
GO

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
GO

CREATE TABLE [shopping_session] (
    [id] int NOT NULL IDENTITY,
    [user_id] int NOT NULL,
    [total] decimal(18,0) NOT NULL,
    [created_at] datetime NOT NULL DEFAULT ((getdate())),
    [modified_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK_shopping_session] PRIMARY KEY ([id]),
    CONSTRAINT [FK_shopping_session_user] FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
);
GO

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
GO

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
GO

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
GO

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
GO

CREATE INDEX [IX_cart_item_session_id] ON [cart_item] ([session_id]);
GO

CREATE UNIQUE INDEX [UQ_cart_item_product_id] ON [cart_item] ([product_id]);
GO

CREATE UNIQUE INDEX [UQ_order_details_payment_id] ON [order_details] ([payment_id]);
GO

CREATE INDEX [IX_order_items_order_id] ON [order_items] ([order_id]);
GO

CREATE UNIQUE INDEX [UQ_order_items_product_id] ON [order_items] ([product_id]);
GO

CREATE INDEX [IX_product_discount_id] ON [product] ([discount_id]);
GO

CREATE UNIQUE INDEX [UQ_product_category_id] ON [product] ([category_id]);
GO

CREATE UNIQUE INDEX [UQ_product_inventory_id] ON [product] ([inventory_id]);
GO

CREATE INDEX [IX_shopping_session_user_id] ON [shopping_session] ([user_id]);
GO

CREATE INDEX [IX_user_address_user_id] ON [user_address] ([user_id]);
GO

CREATE INDEX [IX_user_payment_user_id] ON [user_payment] ([user_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240503151943_Initial', N'8.0.4');
GO

COMMIT;
GO

