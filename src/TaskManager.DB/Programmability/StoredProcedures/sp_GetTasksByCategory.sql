CREATE PROCEDURE [dbo].[sp_GetTasksByCategory]
	@CategoryId int
AS
BEGIN
	SELECT * FROM [dbo].[Tasks] t
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = t.UserId
	WHERE t.UserId = @CategoryId
END
