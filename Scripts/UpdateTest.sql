
WAITFOR DELAY '00:00:10';
select ROW_NUMBER() OVER (
	ORDER BY [Name]
   ) row_num, [Name], OID  
from Customer

Select * from Customer order by CustomOrder

update c set CustomOrder = 0 from Customer c 