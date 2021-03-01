--/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT TOP (1000) [name]
--      ,[rating]
--      ,[root_domain]
--      ,[logo_file]
--      ,[description]
--  FROM [dbo].[JobBoards]
--GO
SELECT [ID]
      ,[JobTitle]
      ,[CompanyName]
      ,[JobURL]
  FROM [dbo].[JobOpportunities]
order by id

--  go
--truncate table jobopportunities
With CTE As
(
  SELECT 
  DISTINCT
	  [ID]
	  ,[JobTitle]
	  ,[CompanyName]
	  ,[JobURL]
	  ,IIF(CHARINDEX('/' + jb.root_domain, jo.JobURL) > 0, name, IIF(CompanyName = 'Unknown' OR CompanyName IS NULL, 'Unknown', 'Company')) as JobSource
  FROM [dbo].[JobBoards] jb RIGHT JOIN [dbo].[JobOpportunities] jo
  ON jb.root_domain = IIF(CHARINDEX(jb.root_domain, jo.JobURL) > 0, jb.root_domain, NULL)
  Where 
  --ID IS NOT NULL AND
  --JobTitle IS NOT NULL AND
  --CompanyName IS NOT NULL AND
  LEN(JobURL) - LEN(REPLACE(JobURL, '/', '')) >=3 AND
  LEN(JobURL) - LEN(REPLACE(JobURL, '.', '')) >=1

)
select * from CTE 
order by ID
