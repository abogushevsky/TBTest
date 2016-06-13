CREATE PROCEDURE [dbo].[sp_DeleteCategory]
	@Id int
AS
	DELETE FROM [dbo].[Categories] WHERE Id = @Id
RETURN @@ROWCOUNT
