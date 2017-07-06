cls

$ProjectName = 'OfficeLocationMicroservice'           #name of project
$baseDir = (resolve-path .)
$teamCityFileLocation = "$baseDir\temp\$ProjectName"  #where the files in the team city agent are located
$targetServerName = '***REMOVED***'   #machine name where we are connecting to

$remoteServerPath = '\\' + $targetServerName + '\c$\temp\' + $ProjectName + '\'

If (Test-Path "$remoteServerPath") {
	Write-Host "Deleting contents: $remoteServerPath"
	Remove-Item "$remoteServerPath\*" -recurse -Force
}
Else {
	Write-Host "Creating folder: $remoteServerPath"
	New-Item -ItemType directory -Path $remoteServerPath
}

#... for testing until branch is merged into develop ...
## ======================================================

$teamCityFileLocation = "$baseDir\.build"  #where the files in the team city agent are located

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


## =====================================================


#copies from team city agent into temporary working directory
Write-Host "copying from $teamCityFileLocation\* to  $remoteServerPath"
Copy-Item "$teamCityFileLocation\*" $remoteServerPath -recurse

$sess = New-PSSession -ComputerName $targetServerName 
write-host "##teamcity[progressStart 'Install of Office Location Microservice to $targetServerName']"



Invoke-Command -Session $sess -ArgumentList ($ProjectName)  -Scriptblock { 
    $ProjectName = $($args[0])

	cd "c:\temp\$ProjectName"

	.\Local_SystemDeployment_Dev.ps1 -projectName $ProjectName

	Write-Host "Build exit code:" $LastExitCode

	# Propagating the exit code so that builds actually fail when there is a problem
	exit $LastExitCode
}

write-host "##teamcity[progressStart 'Done with remote install of Office Location Microservice to $targetServerName']"
