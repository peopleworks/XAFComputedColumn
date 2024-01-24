SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================
-- Author:		Xari + BitFramework
-- Create date: <Create Date,,>
-- Description:	Update CustomOrder with sequence generate by customer name. 
-- =========================================================================
ALTER PROCEDURE UpdateCustomerCustomOrder
AS
BEGIN
        SET NOCOUNT ON;
        --
        WAITFOR DELAY '00:00:30';
		update c set CustomOrder = isnull(cn.row_num,0) 
		from Customer c
		inner join 
		(
        select
                ROW_NUMBER() OVER ( ORDER BY [Name] ) row_num,
                [Name]                                       ,
                OID
        from Customer
		) cn on c.OID = cn.OID

END 
