USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_GetUserRole]    Script Date: 04/02/2018 10:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Acosta,Leo
-- Create date: 03-27-2018
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_GetUserRole]
 @User		NVARCHAR(25)	
AS


	SET NOCOUNT ON;

BEGIN
	DECLARE @RoleName NVARCHAR(25)
	
	SELECT @RoleName = [ROLE] FROM SpaceMgt.dbo.GC_USER 
	WHERE Partner_ID = @User AND ACTIVE = 1
	
	if @RoleName IS NOT NULL 
	BEGIN
		UPDATE SpaceMgt.dbo.GC_USER SET Logon_Date = GETDATE()
		WHERE PARTNER_ID = @User AND ACTIVE = 1
		
	END		
		SELECT CASE @RoleName
		WHEN 'GUEST USER' THEN '1'
		WHEN 'POWER USER' THEN '2'	
		else '0' END

		
		
END	
	
			




