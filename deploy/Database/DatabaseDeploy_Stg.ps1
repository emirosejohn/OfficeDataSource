param(
	[String]$projectName="OfficeLocationMicroservice"
)
cls


$baseDir = (resolve-path .)
$teamCityFileLocation = "$baseDir\temp\$ProjectName" 

cd $teamCityFileLocation

Get-Location

#$nugetExe = (get-childItem (".\lib\nuget\NuGet.exe")).FullName
#&$nugetExe "restore" ".\src\build\packages.config" "-outputDirectory" ".\src\packages"

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem (".\lib\psake\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

# you can put arguments to task in multiple lines using `
Invoke-psake -buildFile .\deploy\Database\default.ps1 `
			 -parameters @{
				 "enviornment" = "STG"
                 "databaseServer" = "***REMOVED***"
                 "projectName" = "$projectName"
				}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode