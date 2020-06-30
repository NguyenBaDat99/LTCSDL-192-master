-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dat	
-- Create date: 30-06
-- Description:	
-- Run: proc_DSProduct '1996-07-19', 1, 99
-- =============================================
ALTER PROCEDURE proc_DSProduct
(
	@ngay datetime,
	@page int,
	@size int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @begin int;
	declare @end int;
	
	set @begin = (@page - 1) * @size + 1;
	set @end = @page * @size;

	with s as (
			select ROW_NUMBER() over (Order by ProductName) as STT, *
			from dbo.Products
			where ProductID not in (select ctdh.ProductID
							from dbo.Orders dh inner join dbo.[Order Details] ctdh on dh.OrderID = ctdh.OrderID 
							where dh.OrderDate = @ngay)
			)	
		select *
		from s
		where STT between @begin and @end
END
GO




-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dat	
-- Create date: 30-06
-- Description:	
-- Run: proc_DSProductKhongConTon 1, 5
-- =============================================
CREATE PROCEDURE proc_DSProductKhongConTon
(
	@page int,
	@size int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @begin int;
	declare @end int;
	
	set @begin = (@page - 1) * @size + 1;
	set @end = @page * @size;

	with s as (
			select ROW_NUMBER() over (Order by ProductName) as STT, *
			from dbo.Products
			where UnitsInStock = 0
			)	
		select *
		from s
		where STT between @begin and @end
END
GO



-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dat	
-- Create date: 30-06
-- Description:	
-- Run: proc_DSOrder 1, 5, 'Andrew'
-- =============================================
CREATE PROCEDURE proc_DSOrder
(
	@page int,
	@size int,
	@keyword nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @begin int;
	declare @end int;
	
	set @begin = (@page - 1) * @size + 1;
	set @end = @page * @size;

	with s as (
			select ROW_NUMBER() over (Order by OrderID) as STT, *
			from dbo.Orders 
			where CustomerID in (select CustomerID from dbo.Customers where CompanyName like '%' + @keyword + '%')
					OR
					EmployeeID in (select EmployeeID from dbo.Employees where FirstName like '%' + @keyword + '%')
			)	
		select *
		from s
		where STT between @begin and @end
END
GO


select dh.OrderDate, Sum(ctdh.Quantity) as TongSoHangCanGiao
from dbo.Orders dh inner join dbo.[Order Details] ctdh on dh.OrderID = ctdh.OrderID
where dh.OrderDate between '1996-07-19' and '1996-09-19'
group by dh.OrderDate
