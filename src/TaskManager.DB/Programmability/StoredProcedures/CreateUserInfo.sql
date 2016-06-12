CREATE PROCEDURE [dbo].[CreateUserInfo]
	@Id nvarchar(128),
	@FirstName nvarchar(64),
	@LastName nvarchar(64)
AS
	INSERT INTO [dbo].[UserInfos] (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)
RETURN @Id
