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
-- Create date: 23-06
-- Description:	
-- Run: proc_ThemSuppiler 'sdfpisfipdsf'
-- =============================================
ALTER PROCEDURE proc_ThemSuppiler
(
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Suppliers(CompanyName) values(@Name)
	select * from Suppliers where CompanyName = @Name
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
-- Create date: 23-06
-- Description:	
-- Run: proc_CapNhatSuppiler 30, 'KDjfso', 'snfosdf', '', 'sfd asd'
-- =============================================
Create PROCEDURE proc_CapNhatSuppiler
(
	-- Add the parameters for the stored procedure here
	@ID int, 
	@CompanyName nvarchar(50),
	@ContactName nvarchar(50),
	@ContactTitle nvarchar(50),
	@Address nvarchar(50)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update Suppliers 
	set CompanyName = @CompanyName,
		ContactName = @ContactName,
		ContactTitle = @ContactTitle,
		Address = @Address
	where SupplierID = @ID

	select * from Suppliers where SupplierID = @ID
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
-- Create date: 23-06
-- Description:	
-- Run: proc_TimSuppiler 1, 5, 'UK'
-- =============================================
Create PROCEDURE proc_TimSuppiler
(
	-- Add the parameters for the stored procedure here
	@page int,
	@size int,
	@keyword nvarchar(50)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @begin int;
	declare @end int;
	
	set @begin = (@page - 1) * @size + 1;
	set @end = @page * @size;
	with s as (
			select ROW_NUMBER() over (Order by CompanyName) as STT, *
			from dbo.Suppliers
			where CompanyName like '%' + @keyword + '%' OR
				Country like '%' + @keyword + '%' 
			)	
		select *
		from s
		where STT between @begin and @end
END
GO




