--
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
-- Create date: 02/06/2020
-- Description:	
-- Test:		exec proc_DoanhThuNhanVienTrongNgay '1996-07-08'
-- =============================================
CREATE PROCEDURE proc_DoanhThuNhanVienTrongNgay
(
	@ngay date
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT nv.EmployeeID, nv.FirstName, nv.LastName, sum((cthd.Quantity*cthd.UnitPrice)*(1-cthd.Discount)) as DoanhThu
	FROM dbo.Employees nv inner join dbo.Orders dh on dh.EmployeeID = nv.EmployeeID
		inner join dbo.[Order Details] cthd on dh.OrderID = cthd.OrderID
	where day(dh.OrderDate) = day(@ngay) AND
			month(dh.OrderDate) = month(@ngay) AND
			year(dh.OrderDate) = year(@ngay)
	group by nv.EmployeeID, nv.FirstName, nv.LastName
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
-- Create date: 02/06/2020
-- Description:	
-- Test:		exec proc_DoanhThuNhanVienTrongKhoang '1996-07-10', '1996-09-08'
-- =============================================
ALTER PROCEDURE proc_DoanhThuNhanVienTrongKhoang
(
	@batdau date,
	@ketthuc date
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT nv.EmployeeID, nv.FirstName, nv.LastName, sum((cthd.Quantity*cthd.UnitPrice)*(1-cthd.Discount)) as DoanhThu
	FROM dbo.Employees nv inner join dbo.Orders dh on dh.EmployeeID = nv.EmployeeID
		inner join dbo.[Order Details] cthd on dh.OrderID = cthd.OrderID
	where dh.OrderDate BETWEEN @batdau AND @ketthuc 
	group by nv.EmployeeID, nv.FirstName, nv.LastName
END
GO

