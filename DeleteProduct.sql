USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_DeleteProduct]    Script Date: 04/02/2018 10:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Leo Acosta
-- Create date: 03-27-2018
-- Description:	Delete A Product
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_DeleteProduct]
( 
    @ID				INTEGER, 
	@UPDATED_BY 	NVARCHAR(100)
   
)
 
AS

BEGIN
	SET NOCOUNT ON;

	
	UPDATE [SpaceMgt].[dbo].[GC_PRODUCT]  set 
			ACTIVE			= '0'		  
			,UPDATED_DATE	= Convert(SMALLDATETIME, GetDate())
			,UPDATED_BY		= @UPDATED_BY 
	WHERE Product_ID = @ID
	

END


