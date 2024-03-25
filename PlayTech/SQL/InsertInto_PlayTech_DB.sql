INSERT INTO [StockShelf] ([Name]) VALUES 
('A'),
('A'),
('A'),
('Б'),
('B'),
('Г'),
('Д'),
('Ж'),
('З')

GO

INSERT INTO [ProductCategory] ([Name], [MainStockShelfId]) VALUES 
('Телефоны', 4),
('Системные блоки', 8),
('Телевизоры', 1),
('Ноутбуки', 1),
('Часы', 8),
('Микрофоны', 8)

GO

INSERT INTO [Product] ([Name], [CategoryId]) VALUES 
('Ноутбук', 4),
('Телевизор', 3),
('Телефон', 1),
('Системный блок', 2),
('Часы', 5),
('Микрофон', 6),
('Samsung 20', 1),
('Iphone 30', 1),
('Samsung TV 20', 3),
('Xiaomi TV 40', 3),
('MiniComp 5', 4),
('Notepad 12', 4),
('Smart seller 100500', 5),
('Sly seller 100500 Pro', 5),
('Good Voice 2000', 6),
('Voice Of The Void', 6),
('Scream 3000', 6)

GO

INSERT INTO [ProductStockShelfBinding] ([ProductId], [StockShelfId]) VALUES 
(1,1),
(2,1),
(4,1),
(5,8),
(6,8),
(3,5),
(3,9),
(5,1)

GO

INSERT INTO [Order] ([CustomerEmail]) VALUES 
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
(''),
('')

GO

INSERT INTO [ShoppingCart] ([OrderId], [ProductId], [StockShelfId], [Count]) VALUES 
(10, 1, 1, 2),
(10, 3, 4, 1),
(10, 6, 8, 1),
(11, 2, 1, 3),
(14, 1, 1, 3),
(14, 4, 8, 4),
(15, 5, 8, 1),
(16, 8, 8, 1),
(16, 9, 8, 1)
