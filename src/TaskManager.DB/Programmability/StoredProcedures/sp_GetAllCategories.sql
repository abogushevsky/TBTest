CREATE PROCEDURE [dbo].[sp_GetAllCategories]
AS
BEGIN
	SELECT * FROM [dbo].[Categories] c
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = c.UserId
END
