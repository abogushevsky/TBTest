CREATE PROCEDURE [dbo].[GetUserInfoById]
	@Id nvarchar(128),
	@param2 int
AS
BEGIN
	SELECT * FROM [dbo].[UserInfos] WHERE Id = @Id
END
