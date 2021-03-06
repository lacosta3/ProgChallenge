USE [SpaceMgt]
GO
/****** Object:  StoredProcedure [dbo].[sp_GC_GetSearchParams]    Script Date: 04/02/2018 10:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Acosta,Leo
-- Create date: 03-27-2018
-- Description:	Gets the Search Parameters
-- =============================================
ALTER PROCEDURE [dbo].[sp_GC_GetSearchParams]
	
AS
BEGIN

	SET NOCOUNT ON;
	

--**********DESCRIPTION
	SELECT DISTINCT [Description] FROM spacemgt.dbo.GC_PRODUCT	
	ORDER BY [Description]
	
	--**********DEPARTMENT
	SELECT DISTINCT [Department] FROM spacemgt.dbo.GC_PRODUCT	
	ORDER BY [Department]
	
	--**********LAST SOLD DATE
	select distinct convert(varchar(25),Last_Sold,101) AS Last_Sold  from spacemgt.dbo.GC_PRODUCT
	ORDER BY Last_Sold
	

END


