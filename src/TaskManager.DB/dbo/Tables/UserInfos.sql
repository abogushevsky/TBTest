CREATE TABLE [dbo].[UserInfos]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(64) NULL, 
    [LastName] NVARCHAR(64) NULL, 
    CONSTRAINT [FK_UserInfos_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [AspNetUsers]([Id])
)
