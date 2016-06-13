CREATE PROCEDURE [dbo].[sp_DeleteTask]
	@Id int
AS
	DELETE FROM [dbo].[Tasks] WHERE Id = @Id
RETURN @@ROWCOUNT
