CREATE PROCEDURE [dbo].[UpdateUserInfo]
	@Id nvarchar(128),
	@FirstName nvarchar(64),
	@LastName nvarchar(64)
AS
	UPDATE [dbo].[UserInfos] SET FirstName = @FirstName, LastName = @LastName
	WHERE Id = @Id
RETURN @@ROWCOUNT
