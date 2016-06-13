CREATE PROCEDURE [dbo].[sp_UpdateCategory]
	@Id int,
	@Name nvarchar(128),
	@UserId nvarchar(128)
AS
	UPDATE [dbo].[Categories] SET Name = @Name, UserId = @UserId
	WHERE Id = @Id
RETURN @@ROWCOUNT
