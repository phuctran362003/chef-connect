CREATE TABLE [Users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [username] varchar(255) UNIQUE NOT NULL,
  [email] varchar(255) UNIQUE NOT NULL,
  [password] varchar(255) NOT NULL,
  [role_id] int NOT NULL,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [Roles] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(50) UNIQUE NOT NULL,
  [description] text,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [ChefInformation] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [user_id] int NOT NULL,
  [bio] text,
  [profile_picture] varchar(255),
  [rating] float DEFAULT (0),
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [Menus] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [chef_id] int NOT NULL,
  [title] varchar(255) NOT NULL,
  [description] text,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [Dishes] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [menu_id] int NOT NULL,
  [name] varchar(255) NOT NULL,
  [description] text,
  [price] decimal(10,2) NOT NULL,
  [image_url] varchar(255),
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [Orders] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [user_id] int NOT NULL,
  [chef_id] int NOT NULL,
  [status] varchar(50) DEFAULT 'pending',
  [total_price] decimal(10,2) NOT NULL,
  [note] text,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [OrderItems] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [order_id] int NOT NULL,
  [dish_id] int NOT NULL,
  [quantity] int NOT NULL,
  [price] decimal(10,2) NOT NULL,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

CREATE TABLE [Admins] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [user_id] int NOT NULL,
  [created_at] datetime DEFAULT (getdate()),
  [updated_at] datetime DEFAULT (getdate())
)
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([role_id]) REFERENCES [Roles] ([id])
GO

ALTER TABLE [ChefInformation] ADD FOREIGN KEY ([user_id]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Menus] ADD FOREIGN KEY ([chef_id]) REFERENCES [ChefInformation] ([id])
GO

ALTER TABLE [Dishes] ADD FOREIGN KEY ([menu_id]) REFERENCES [Menus] ([id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([user_id]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([chef_id]) REFERENCES [ChefInformation] ([id])
GO

ALTER TABLE [OrderItems] ADD FOREIGN KEY ([order_id]) REFERENCES [Orders] ([id])
GO

ALTER TABLE [OrderItems] ADD FOREIGN KEY ([dish_id]) REFERENCES [Dishes] ([id])
GO

ALTER TABLE [Admins] ADD FOREIGN KEY ([user_id]) REFERENCES [Users] ([id])
GO
