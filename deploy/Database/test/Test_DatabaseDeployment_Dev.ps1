param(
	[String]$projectName="OfficeLocationMicroservice"
)
cls


# =================== Run Local Build ===================================

$nugetExe = (get-childItem (".\src\.NuGet\NuGet.exe")).FullName
&$nugetExe "restore" ".\src\build\packages.config" "-outputDirectory" ".\src\packages"

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem (".\src\Packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

# you can write statements in multiple lines using `
Invoke-psake -buildFile ./default.ps1 `
			 -taskList teamcity `
             -parameters @{
                "projectVersion" = "101"
                } `
			 -framework 4.6

Write-Host "Build exit code:" $LastExitCode

# =================== Run Local Build ===================================




# you can put arguments to task in multiple lines using `
Invoke-psake -buildFile .\deploy\Database\default.ps1 `
			 -parameters @{
				 "enviornment" = "DEV"
                 "databaseServer" = "***REMOVED***"
                 "projectName" = "$projectName"
				}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode