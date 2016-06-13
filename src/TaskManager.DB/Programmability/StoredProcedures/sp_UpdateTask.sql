CREATE PROCEDURE [dbo].[sp_UpdateTask]
	@Id int,
	@Title nvarchar(128),
	@Details nvarchar(max),
	@DueDate datetime,
	@CategoryId int,
	@UserId nvarchar(128)
AS
	UPDATE [dbo].[Tasks] SET Title = @Title, Details = @Details, DueDate = @DueDate, CategoryId = @CategoryId, UserId = @UserId
	WHERE Id = @Id
RETURN @@ROWCOUNT
