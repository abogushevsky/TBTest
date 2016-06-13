﻿CREATE PROCEDURE [dbo].[sp_GeTaskById]
	@Id int
AS
BEGIN
	SELECT 
		t.Id AS Id, 
		t.Title AS Title,
		t.Details AS Details,
		t.DueDate AS DueDate,
		u.Id AS UserId,
		u.FirstName AS UserFirstName,
		u.LastName AS UserLastName,
		c.Id AS CategoryId,
		c.Name AS CategoryName,
		t.ModifiedTimestamp as ModifiedTimestamp
	FROM [dbo].[Tasks] t
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = t.UserId
	INNER JOIN [dbo].[Categories] c ON c.Id = t.CategoryId
END
