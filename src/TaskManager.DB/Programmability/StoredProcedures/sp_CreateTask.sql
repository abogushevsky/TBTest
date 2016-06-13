CREATE PROCEDURE [dbo].[sp_CreateTask]
	@Title nvarchar(128),
	@Details nvarchar(max),
	@DueDate datetime,
	@CategoryId int,
	@UserId nvarchar(128)
AS
	INSERT INTO [dbo].[Tasks] (Title, Details, DueDate, CategoryId, UserId) 
	VALUES (@Title, @Details, @DueDate, @CategoryId, @UserId)
RETURN SCOPE_IDENTITY()
