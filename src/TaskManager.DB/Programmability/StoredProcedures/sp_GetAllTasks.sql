CREATE PROCEDURE [dbo].[sp_GetAllTasks]
AS
BEGIN
	SELECT * FROM [dbo].[Tasks] t
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = t.UserId
END
