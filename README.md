# README

Emily Johnson

Max Booth

July 2017



## New Developer Setup Instructions
We use develop as our main branch to be deployed into the DEV environment. Create a new branch feature/[your_feature] 
for each new step. We use GitExtensions and Visual Studio to handle commits and coding. Most files are 
in C#, although we have three SQL scripts and the views use HTML, CSS, Javascript, and JQuery.

WebUI is the main project that runs our UI. We have one Model, OfficeModel that holds all of the Dimensional 
Offices which are represented by the OfficeLocation class (in the Core). The Views are split into Layout 
& Error (which are shared by each webpage) in OfficeAPI and Index (the main page) in OfficeLocation. The 
controllers are also in WebUI.

Email project acts like a controller to send update emails once an office is saved.
Data project contains the Gateways that access the databases CountryMicroservice and our database 
OfficeLocationMicroservice. OfficeDataTableGateway handles the SQL calls to get OfficeDtos from our 
database. CountryWebApiGateway gets the RegionSchemes from CountryMicroservice in order to use the 
slugs. The class hierarchy in the Country database is as so: RegionScheme>Region>Country>Name,CountryCodes,
Slug where slug is the readable ID for each country code.

Core project in the Business Rules folder organizes data. In the Domain folder, our data is stored as the objects 
OfficeLocation and Country where all Office attributes in the OfficeLocation object per office 
and the Country attributes in Country object per country. The Repositories call their respective Gateways 
to init and update these objects.
In the Services folder, we have the OfficeDto, OperatingOptions, RegionSchemeResponseJson, and all of the Interfaces.

The Tests folders (IntegrationTests and UnitTests) contain their own projects. We do not yet have any 
meaningful unit tests but we have set up a UnitTests project. Under Integration Tests, we test the API, 
Repositories, and Controllers. Only tests on UI are through the controller tests. We strive for Test Driven 
Development.

**We have included a workflow chart (Location-Workflow) for future integration with systems used in 
Dimensional by department** courtesy of Hal Norris (***REMOVED***) who was the Business Analyst intern 
working under Julie Saft in Technology Planning and Strategy.

## Project specific Rules and Policies
Developers must follow the Philosophy of the Coding Standards. We follow MVC design where the models, views 
and controllers are in WebUI folder and back end in Core. Developers should take all Resharper formatting 
suggestions unless it makes the code less readable. Each office has a unique ID and should have unique Office 
Name and Address. Each Office is required to have a name, address, switchboard (phone number) and operating 
status. The operating status can either be Active or Closed; we do not delete any offices in order to minimize 
discrepancies and maintain historical records and thus set them to closed instead.

Tests must have a good name, clearly defining what the test means and the behavior. Test must also be contained 
in the appropriate class, with other cases testing the same unit.

## The location of the Build system for that project
the main directory holds our powershell scripts and the .build folder. To run tests use .build, to 
reset the database s.t. the project is usable, run .build.DatabaseOnly

## Important contacts 
Paul Herrera ***REMOVED*** is the lead developer and will oversee the continuation of 
this project. Max Booth (unrelated to David) and Emily Johnson also have priveledges to pull requests;
however, we will most likely lose this once internship is over. Corporate Services, specifically Karen 
Hernandez (Head of Hospitality Services-Americas ***REMOVED***) and Jenny Hill 
(***REMOVED***), are responsible foer managing the office database through the UI only.

## A list of deployable pieces (windows services, databases websites) and the servers used in each environment
Deployable: 
* Email
* Website through Index.cshtml (dev http://***REMOVED***:1704/ and stg http://***REMOVED***:1704/)
* OfficeLocationMicroservice Database ( ***REMOVED*** and ***REMOVED***)

Servers:
* Local machine
* Web Deployment - Dev
* Web Deployment - Stg
* Web Deployment - Prd
* Database Deployment - Dev
* Database Deployment - Stg
* Database Deployment - Prd

## A description of the design/technical goals of the system
The database is initialized in SQL data/mssql/OfficeLocationMicroservice/Up/0001_CREATETABLES.sql 
and the addresses are updated with correct formatting using 0002_ALTERTABLES.sql and the slugs using 
003_AddingCountrySlugs in the same file. Design is noted under SetUp. The technical goal of this project 
is to have a central database that keeps records of Dimensional Offices and their locations around the world. 
We have implemented a UI in order for Corporate Services to keep this information up to date. The project is 
deployed through TeamCity and Paul will maintain the back end of the project. This database/service will 
ideally be used in all Dimensional applications that involve office locations. **We have included a workflow 
chart (Location-Workflow) for future integration with systems used in Dimensional by department.** We are working 
to get the OfficeLocation data into a form that is usable for Sharepoint.

