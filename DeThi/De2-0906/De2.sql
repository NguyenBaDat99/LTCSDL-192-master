USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[proc_DanhSachDHTrongKhoang]    Script Date: 09-Jun-20 1:30:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dat
-- Create date: 09-06
-- Description:	
-- Test: exec proc_DanhSachDHTrongKhoang '1997-07-08', '1997-12-08'
-- =============================================
ALTER PROCEDURE [dbo].[proc_DanhSachDHTrongKhoang]
(
	@begin datetime,
	@end datetime
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	from dbo.Orders
	where OrderDate between @begin and @end
END


--================================================================================

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
-- Create date: 09-06
-- Description:	
-- Test: exec proc_DanhSachChiTietDH 10249
-- =============================================
CREATE PROCEDURE proc_DanhSachChiTietDH
(
	@id int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	from dbo.[Order Details]
	where OrderID = @id
END
GO

