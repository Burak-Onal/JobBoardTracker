a.	Instructions on how to run the code
1) Clone the solution into your local repo
2) Run both JobBoardTracker and JobBoardTrackerAPI
	I)Click right on the solution 
	II) Select Multiple startup projects
	III) Select Start for both JobBoardTracker and JobBoardTrackerAPI and click OK
3) Click Start to run the app
4) Click on Job Board in the header
5) Click on one of the JobBoard tiles
6) Verify data is displayed

b.	If you used any third party libraries or packages please list which ones and why
- No extra packages are used.

c.	A brief explanation of how your program works and why this is an effective implementation. 
- The application is developed by using ASP.NET Core Web API, ASP.NET Core MVC, SSIS, SQL Server and Azure
- The data provided in the job_opportunities.csv is refined in the SSIS and the data is insterted in the database which is hosted in the Azure.
- The data stored in the JobOpportunities.csv is inserted in the JobOpportunities table via SSIS package called SSISJobBoardResolver. 
- SSIS package uses the Stored Procedure called PopulateJobSourceResolutionTbl to populate the JobSourceResolution table. 
- JobSourceResolution table includes ID, JobTitle, CompanyName, JobURL and Source columns. This stored procedure eliminates invalid URLs which has null values or incomplete URLs such as "http://".
- I populated the table called JobBoards from the JSON file job_boards.json which is provided in the assignment. 
- A null check is applied on the ID during the population in the package.
- The API called JobBoardTrackerAPI includes two methods called GetAllJobBoards and GetJobsBySource. 
  GetAllJobBoards retrieves the data from JobBoards table and 
  GetJobsBySource calls the stored procedure called GerJobsBySource to retrieve the data from JobSourceResolution. GerJobsBySource eliminates the duplicate data.
- The client-side app called JobBoardTracker has two main pages; 1st JobBoard and 2nd Jobs.
- JobBoard displays the tiles for each job board with picture and descriptions and Jobs page separately displays the jobs for each job board.

d.	A publicly accessible web URL to view Part 2: Building the interface
- GIT REPO: https://github.com/Burak-Onal/JobBoardTracker

e.	A CSV structured similar to sample_job_source_resolution_data.csv for the 
- Please see job_source_resolution_data.csv.xlsx in the repo. The duplicate an invalid data is eliminated in this file. Count is 18743.


f.	A table or some other minimal visual representation showing the job source and the total count of job opportunities associated with that job source. Here’s a partial example. 
- Please see count of jobs per source.xlsx in the repo

EXTRA QUESTIONS
-	How would you determine if a job application link is still active?
	The app can make a call to the URL and read if it returns 404. If so the link is inactive.

