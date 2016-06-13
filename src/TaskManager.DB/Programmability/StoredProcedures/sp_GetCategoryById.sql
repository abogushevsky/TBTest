CREATE PROCEDURE [dbo].[sp_GetCategoryById]
	@Id int
AS
BEGIN
	SELECT * FROM [dbo].[Categories] c
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = c.UserId
	WHERE c.Id = @Id
END
