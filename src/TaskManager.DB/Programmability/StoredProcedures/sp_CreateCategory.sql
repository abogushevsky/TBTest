CREATE PROCEDURE [dbo].[sp_CreateCategory]
	@Name nvarchar(128),
	@UserId nvarchar(128)
AS
	INSERT INTO [dbo].[Categories] (Name, UserId) 
	VALUES (@Name, @UserId)
RETURN SCOPE_IDENTITY()
