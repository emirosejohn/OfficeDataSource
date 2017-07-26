param(
	[String]$projectName="OfficeLocationMicroservice"
)
cls

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path

$psakeModule = (Get-ChildItem (".\lib\psake\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

# you can put arguments to task in multiple lines using `
Invoke-psake -buildFile .\deploy\Web\default.ps1 `
			 -parameters @{
				 "enviornment" = "PRD"
                 "databaseServer" = "***REMOVED***"
                 "projectName" = "$projectName"
                 "CountryWebApiUrl" = "***REMOVED***"

                 "EmailServerName" = "***REMOVED***"
                 "EmailTo"= "***REMOVED***, Global_Assistants.com"
                 "EmailFrom" = "***REMOVED***"

                 "AdminGroup" = "***REMOVED***"
				}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode