CREATE PROCEDURE [dbo].[sp_GetUserCategories]
	@UserId nvarchar(128)
AS
BEGIN
	SELECT * FROM [dbo].[Categories] c
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = c.UserId
	WHERE c.UserId = @UserId
END
