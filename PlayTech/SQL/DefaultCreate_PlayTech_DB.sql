IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'PlayTech')
  BEGIN
    CREATE DATABASE [PlayTech];
	END
GO

USE [PlayTech]

CREATE TABLE [StockShelf]
(
	[StockShelfId] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR (10)
)

GO

USE [PlayTech]

CREATE TABLE [ProductCategory]
(
	[ProductCategoryId] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR (30),
	[MainStockShelfId] INT

	CONSTRAINT "FK_ProductCategory_StockShelf" FOREIGN KEY ([MainStockShelfId]) REFERENCES [StockShelf] ([StockShelfId])
)

GO

USE [PlayTech]

CREATE TABLE [Product]
(
	[ProductId] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR (100),
	[CategoryId] INT NOT NULL,

	CONSTRAINT "FK_Product_ProductCategory" FOREIGN KEY ([CategoryId]) REFERENCES [ProductCategory] ([ProductCategoryId])
)

GO 


USE [PlayTech]

CREATE TABLE [ProductStockShelfBinding]
(
	[ProductId] INT NOT NULL,
	[StockShelfId] INT NOT NULL

	CONSTRAINT "UQ_ProductId_StockShelfId" UNIQUE ([ProductId], [StockShelfId]),
	CONSTRAINT "FK_ProductStockShelfBinding_Product" FOREIGN KEY  ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT "FK_ProductStockShelfBinding_StockShelf" FOREIGN KEY  ([StockShelfId]) REFERENCES [StockShelf] ([StockShelfId]) ON DELETE CASCADE ON UPDATE CASCADE
)


GO

USE [PlayTech]

CREATE TABLE [Order]
(
	[OrderId] INT PRIMARY KEY IDENTITY,
	[CustomerEmail] NVARCHAR (50) NULL
)

GO

USE [PlayTech]

CREATE TABLE [ShoppingCart]
(
	[ShoppingCartId] INT PRIMARY KEY IDENTITY,
	[OrderId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[StockShelfId] INT NOT NULL,
    [Count] INT NOT NULL DEFAULT 1,

	CONSTRAINT "UQ_OrderId_ProductId_StockShelfId" UNIQUE ([OrderId],[ProductId],[StockShelfId]),
	CONSTRAINT "FK_ShoppingCart_Order" FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]),
	CONSTRAINT "FK_ShoppingCart_Product" FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]),
	CONSTRAINT "FK_ShoppingCart_StockShelf" FOREIGN KEY ([StockShelfId]) REFERENCES [StockShelf] ([StockShelfId])
)