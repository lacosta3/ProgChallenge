USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_UpdateProduct]    Script Date: 04/02/2018 10:35:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Leo Acosta
-- Create date: 03-27-2018
-- Description:	Update A Product
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_UpdateProduct]
( 
    @ID				INTEGER, 
	@UPDATED_BY 	NVARCHAR(200),
	@DESCRIPTION 	NVARCHAR(200),
	@LASTSOLD	 	DATE,
	@DEPARTMENT	 	NVARCHAR(100),
	@SHELFLIFE	 	NVARCHAR(500),
	@PRICE		 	NVARCHAR(200),
	@UNIT		 	NVARCHAR(200),
	@XFOR 			INTEGER,
	@COST		 	NVARCHAR(20)   
)
 
AS

BEGIN
	SET NOCOUNT ON;
	
	UPDATE [SpaceMgt].[dbo].[GC_PRODUCT]  SET 
			[Description] 	= UPPER(@DESCRIPTION)
			,Department 	= UPPER(@DEPARTMENT	)
			,Last_Sold		= @LASTSOLD
			,Shelf_Life		= @SHELFLIFE
			,Price			= @PRICE
			,Unit			= UPPER(@UNIT)
			,xFor			= @XFOR
			,Cost			= @COST
			,Updated_Date	= Convert(SMALLDATETIME, GetDate())
			,Updated_By		= UPPER(@UPDATED_BY) 
	WHERE Product_ID = @ID
	


END


