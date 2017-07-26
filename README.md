# README

Max Booth
Emily Johnson
July 2017


readme.md file at the root. 

## New Developer Setup Instructions
We use develop as our main branch to be deployed into the DEV environment. Create a new branch feature/[your_feature] 
for each new step. We use GitExtensions and Visual Studio to handle commits and coding. Most files are in C#, although 
we have three SQL scripts and the views use HTML, CSS, Javascript, and JQuery. 

The Tests folders (IntegrationTests and UnitTests) are outside the WebUI project. We do not yet have any 
meaningful unit tests but we have set up a UnitTests project. Under Integration Tests, we test the API, 
Repositories, and Controllers. Only tests on UI are through the controllers.

## Project specific rules and policies
Must follow the Philosophy of the Coding Standards. We follow MVC design where the models and views are
in WebUI folder and back end in Core. Developers should take all Resharper formatting suggestions unless 
it makes the code less readable.

## The location of the Build system for that project
the main directory holds our powershell scripts and the .build folder. To run tests use .build, to 
reset the database s.t. the project is usable, run .build.DatabaseOnly

## Important contacts 
Paul Herrera paul.herrera@dimensional.com is the lead developer and will oversee the continuation of 
this project. Max Booth (unrelated to David) and Emily Johnson also have priveledges to pull requests;
however, we will most likely lose this once internship is over. 

## Pull request SLA (Service Level Agreement)

## A list of deployable pieces (windows services, databases websites) and the servers used in each environment

## A description of the design/technical goals of the system
The database is initialized in SQL data/mssql/OfficeLocationMicroservice/Up/0001_CREATETABLES.sql 
and the addresses are updated with correct formatting using 0002_ALTERTABLES.sql in the same file.
WebUI is the main project that runs our UI. 



Have a good name, clearly defining what the test means and the behavior being tests
 Be contained in the appropriate class, with other cases testing the same unit.

## Optional link to roadmap