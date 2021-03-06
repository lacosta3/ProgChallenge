USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_GetProductList]    Script Date: 04/02/2018 10:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Acosta, Leo
-- Create date: 03-27-2018
-- Description:	Gets Tasklist
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_GetProductList] 
 @DESCRIPTION	nvarchar(250),
 @DEPT			nvarchar(100),
 @LASTSOLD		nvarchar(100)

 
AS
SET NOCOUNT ON;

	if (@DESCRIPTION = 'ALL') 
		SET @DESCRIPTION = NULL
		
	if (@DEPT = 'ALL') 
		SET @DEPT = NULL	
		
	if (@LASTSOLD = 'ALL') 
		SET @LASTSOLD = NULL	
 

IF(@DESCRIPTION IS NULL AND @DEPT IS NULL AND @LASTSOLD IS NULL )
	BEGIN
			
		SELECT A.Product_ID, A.UPC,A.[Description], A.Department, A.Shelf_Life, A.Last_Sold, A.Price,
		A.Unit,A.xFor,A.Cost, A.Active		
		FROM SpaceMgt.dbo.GC_PRODUCT A
		WHERE Active = 1 
		ORDER BY A.Last_Sold,A.[Description]
		
	END
		
ELSE
	BEGIN

			
		SELECT A.Product_ID, A.UPC,A.[Description], A.Department, A.Shelf_Life, A.Last_Sold, A.Price,
		A.Unit,A.xFor,A.Cost, A.Active		
		FROM SpaceMgt.dbo.GC_PRODUCT A
		WHERE	 
		((@DESCRIPTION IS NULL) OR (A.[Description] = @DESCRIPTION)) AND 
		((@DEPT IS NULL) OR (A.Department = @DEPT)) AND 
		((@LASTSOLD IS NULL) OR (A.Last_Sold = @LASTSOLD)) 
		ORDER BY A.Last_Sold,A.[Description]
			
		
		END

