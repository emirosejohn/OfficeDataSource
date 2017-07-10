#Can run this test from local machien rather than having to run on team city.

param(
	[String]$ProjectName = 'OfficeLocationMicroservice',           #name of project
    [String]$targetServerName = '***REMOVED***'                    #machine name where we are connecting to
)
cls

$baseDir = (resolve-path .)
$FileLocation = "$baseDir\.build"                                  #where the files in the team city agent are located
$remoteServerPath = '\\' + $targetServerName + '\c$\temp\' + $ProjectName + '\'

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


# =================== Deploy To Website =================================
If (Test-Path "$remoteServerPath") {
	Write-Host "Deleting contents: $remoteServerPath"
	Remove-Item "$remoteServerPath\*" -recurse -Force
}
Else {
	Write-Host "Creating folder: $remoteServerPath"
	New-Item -ItemType directory -Path $remoteServerPath
}

#copies from team city agent into temporary working directory
Write-Host "copying from $FileLocation\* to  $remoteServerPath"
Copy-Item "$FileLocation\*" $remoteServerPath -recurse

$sess = New-PSSession -ComputerName $targetServerName 

Invoke-Command -Session $sess -ArgumentList ($ProjectName)  -Scriptblock { 
    $ProjectName = $($args[0])

	cd "c:\temp\$ProjectName"


	.\deploy\Web\Local_WebDeploy_Dev.ps1 -projectName $ProjectName

	Write-Host "Build exit code:" $LastExitCode

	# Propagating the exit code so that builds actually fail when there is a problem
	exit $LastExitCode
}

# =================== Deploy To Website =================================