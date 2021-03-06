USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_AddProductFile]    Script Date: 04/02/2018 10:32:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Acosta, Leo
-- Create date: 3-29-2018
-- Description:	Insert a Product File
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_AddProductFile] 
    @tbl GC_Product_Upload READONLY

AS
--SET NOCOUNT ON;

	BEGIN
		
		INSERT INTO SpaceMgt.dbo.GC_PRODUCT(UPC,[Description],Department,Shelf_Life,Last_Sold,Price,Unit,
				xFor,Cost,Created_By,[User_ID],Created_Date)
		SELECT UPC, upper([DESCRIPTION]), upper(DEPARTMENT), SHELFLIFE, LAST_SOLD, PRICE, upper(UNIT), XFOR, COST, CREATED_BY,PARTNER_ID, GETDATE()		
		FROM @tbl
		

		--RETURN 
		SELECT 1
	END



