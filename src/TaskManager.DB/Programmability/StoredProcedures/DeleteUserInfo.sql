CREATE PROCEDURE [dbo].[DeleteUserInfo]
	@Id nvarchar(128)
AS
	DELETE FROM [dbo].[UserInfos] WHERE Id = @Id
RETURN @@ROWCOUNT
