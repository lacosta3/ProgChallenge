USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_RegisterNewUser]    Script Date: 04/02/2018 10:34:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Acosta, Leo
-- Create date: 03-27-2018
-- Description:	Inserts, by passing a Table Type 
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_RegisterNewUser] 
      @tbl GC_Register_User READONLY

AS
 SET NOCOUNT ON;
	DECLARE @userid nvarchar(25)
	set @userid = (SELECT PARTNER_ID FROM @tbl)
	
	if not exists(SELECT 1 FROM SpaceMgt.dbo.GC_USER WHERE Partner_ID = @userid)
	 BEGIN     
		INSERT INTO SpaceMgt.dbo.GC_USER (PARTNER_ID,FIRST_NAME,LAST_NAME,EMAIL,[ROLE],CREATED_BY,CREATED_DATE,LOGON_DATE) 
		  SELECT PARTNER_ID,UPPER(FIRST_NAME),UPPER(LAST_NAME),LOWER(EMAIL),'GUEST USER',CREATED_BY,GETDATE(),GETDATE() FROM @tbl
	END

		



		 








