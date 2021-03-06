USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_AddNewProduct]    Script Date: 04/02/2018 10:30:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Leo Acosta
-- Create date: 03-27-2018
-- Description:	Add A Product
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_AddNewProduct]
( 
    @UPC			NVARCHAR(50), 
	@UPDATED_BY 	NVARCHAR(150),
	@PARTNERID  	NVARCHAR(50),
	@DESCRIPTION 	NVARCHAR(150),
	@LASTSOLD	 	DATE,
	@DEPARTMENT	 	NVARCHAR(100),
	@SHELFLIFE	 	NVARCHAR(50),
	@PRICE		 	NVARCHAR(50),
	@UNIT		 	NVARCHAR(25),
	@XFOR 			INTEGER,
	@COST		 	NVARCHAR(50)   
)
 
AS

BEGIN
	SET NOCOUNT ON;
	INSERT INTO [SpaceMgt].[dbo].[GC_PRODUCT] (UPC,[Description],Department,Last_Sold,Shelf_Life,Price,Unit,
				xFor,Cost,Created_Date,Created_By,[User_ID])
		VALUES			
		(@UPC,UPPER(@DESCRIPTION),UPPER(@DEPARTMENT	),@LASTSOLD,@SHELFLIFE,@PRICE,UPPER(@UNIT),@XFOR,@COST,
		Convert(SMALLDATETIME, GetDate()),@UPDATED_BY,@PARTNERID )	

END


