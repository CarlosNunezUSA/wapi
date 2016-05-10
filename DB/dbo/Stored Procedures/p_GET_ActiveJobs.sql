-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.p_GET_ActiveJobs 
AS
BEGIN

	SET NOCOUNT ON;
    SELECT * FROM [Dashboard].[dbo].[Job]
    WHERE IsEnabled = 1
  
END