CREATE PROCEDURE [dbo].[sp_UpdateTask]
	@Id int,
	@Title nvarchar(128),
	@Details nvarchar(max),
	@DueDate datetime,
	@CategoryId int,
	@UserId nvarchar(128),
	@ModifiedTimestamp timestamp
AS
BEGIN
	DECLARE @rowVersion timestamp;
	SELECT @rowVersion = (SELECT ModifiedTimestamp FROM [dbo].[Tasks])
	IF @ModifiedTimestamp != @rowVersion
		RAISERROR(50005, 1, 1);

	UPDATE [dbo].[Tasks] SET Title = @Title, Details = @Details, DueDate = @DueDate, CategoryId = @CategoryId, UserId = @UserId
	WHERE Id = @Id
	SELECT @@ROWCOUNT AS Result
END
