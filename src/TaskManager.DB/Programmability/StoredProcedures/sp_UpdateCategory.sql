CREATE PROCEDURE [dbo].[sp_UpdateCategory]
	@Id int,
	@Name nvarchar(128),
	@UserId nvarchar(128),
	@ModifiedTimestamp timestamp
AS
BEGIN
	DECLARE @rowVersion timestamp;
	SELECT @rowVersion = (SELECT ModifiedTimestamp FROM [dbo].[Tasks])
	IF @ModifiedTimestamp != @rowVersion
		RAISERROR(50005, 1, 1);

	UPDATE [dbo].[Categories] SET Name = @Name, UserId = @UserId
	WHERE Id = @Id
	SELECT @@ROWCOUNT AS Result
END