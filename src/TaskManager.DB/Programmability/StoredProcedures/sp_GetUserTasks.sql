CREATE PROCEDURE [dbo].[sp_GetUserTasks]
	@UserId nvarchar(128)
AS
BEGIN
	SELECT * FROM [dbo].[Tasks] t
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = t.UserId
	WHERE t.UserId = @UserId
END
