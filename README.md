# README

Max Booth

Emily Johnson

July 2017


readme.md file at the root. 

## New Developer Setup Instructions
We use develop as our main branch to be deployed into the DEV environment. Create a new branch feature/[your_feature] 
for each new step. We use GitExtensions and Visual Studio to handle commits and coding. Most files are 
in C#, although we have three SQL scripts and the views use HTML, CSS, Javascript, and JQuery.

WebUI is the main project that runs our UI. We have one Model, OfficeModel that holds all offices which
are represented by the OfficeLocation class (in the Core). The Views are split into shared Layout & Error 
in OfficeAPI and Index (the main page) in OfficeLocation.

Email project acts like controller to send update emails once an office is saved.
Data project contains the Gateways that access the databases CountryMicroservice and our database 
OfficeLocationMicroservice. OfficeDataTableGateway handles the SQL calls to get OfficeDtos from our 
database. CountryWebApiGateway gets the RegionSchemes from CountryMicroservice in order to use the 
slugs. The class hierarchy as so RegionScheme>Region>Country>Name,CountryCodes,Slug where slug is the 
readable ID for each country code.

Core project in Business Rules contains all the tangible data. in Domain, our data is stored as objects 
OfficeLocation and Country where we have all Office attributes in the OfficeLocation object per office 
and the Country attributes in Country object per country. The respective Repositories call their Gateways 
to init these objects.
In Services, we have the OfficeDto, OperatingOptions, RegionSchemeResponseJson, and all of the Interfaces.

The Tests folders (IntegrationTests and UnitTests) are outside the WebUI project. We do not yet have any 
meaningful unit tests but we have set up a UnitTests project. Under Integration Tests, we test the API, 
Repositories, and Controllers. Only tests on UI are through the controllers. We strive for Test Driven 
Development.

## Project specific rules and policies
Must follow the Philosophy of the Coding Standards. We follow MVC design where the models and views are
in WebUI folder and back end in Core. Developers should take all Resharper formatting suggestions unless 
it makes the code less readable.
Each office has a unique ID and should have unique Office Name and Address. Each Office is required to have 
a name, address, switchboard (phone number) and operating status. The operating status can either be Active 
or Closed; we do not delete any offices in order to minimize discrepancies and set them to closed instead.

## The location of the Build system for that project
the main directory holds our powershell scripts and the .build folder. To run tests use .build, to 
reset the database s.t. the project is usable, run .build.DatabaseOnly

## Important contacts 
Paul Herrera ***REMOVED*** is the lead developer and will oversee the continuation of 
this project. Max Booth (unrelated to David) and Emily Johnson also have priveledges to pull requests;
however, we will most likely lose this once internship is over. Corporate Services, specifically Karen 
Hernandez (Head of Hospitality Services-Americas ***REMOVED***) and Jenny Hill 
(***REMOVED***), are responsible foer managing the office database through the UI only.

## Pull request SLA (Service Level Agreement)

## A list of deployable pieces (windows services, databases websites) and the servers used in each environment

## A description of the design/technical goals of the system
The database is initialized in SQL data/mssql/OfficeLocationMicroservice/Up/0001_CREATETABLES.sql 
and the addresses are updated with correct formatting using 0002_ALTERTABLES.sql in the same file.
WebUI is the main project that runs our UI. 



Have a good name, clearly defining what the test means and the behavior being tests
 Be contained in the appropriate class, with other cases testing the same unit.

## Optional link to roadmap