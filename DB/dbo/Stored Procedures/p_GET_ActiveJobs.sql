-- =============================================
-- Author:		Carlos Nunez
-- Create date: 5/10/2016
-- Description:	Get Active Jobs
-- =============================================
CREATE PROCEDURE dbo.p_GET_ActiveJobs 
AS
BEGIN

	SET NOCOUNT ON;
    SELECT * FROM [Dashboard].[dbo].[Job]
    WHERE IsEnabled = 1
  
END