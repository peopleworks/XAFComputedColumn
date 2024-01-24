SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================
-- Author:		Xari + BitFramework
-- Create date: <Create Date,,>
-- Description:	Update CustomOrder with sequence generate by customer name. 
--              Using Job
-- =========================================================================
ALTER PROCEDURE UpdateCustomerCustomOrderJob 
	
AS
BEGIN
	
	SET NOCOUNT ON;
	EXEC msdb.dbo.sp_start_job N'Update Customer CustomOrder in Customer';
END
GO
