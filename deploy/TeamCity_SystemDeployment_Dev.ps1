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

#copies from team city agent into temporary workign directory
Write-Host "copying from $teamCityFileLocation\* to  $remoteServerPath"
Copy-Item "$teamCityFileLocation\*" $remoteServerPath -recurse

$sess = New-PSSession -ComputerName $targetServerName 
write-host "##teamcity[progressStart 'Install of Investor Reporting to $targetServerName']"

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem (".\src\Packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
 
Invoke-Command -Session $sess -ArgumentList ($ProjectName)  -Scriptblock { 
    $ProjectName = $($args[0])

	cd "c:\temp\$ProjectName"

	.\Local_SystemDeployment_Dev.ps1 -projectName $ProjectName

	Write-Host "Build exit code:" $LastExitCode

	# Propagating the exit code so that builds actually fail when there is a problem
	exit $LastExitCode
}

write-host "##teamcity[progressStart 'Done with remote install of Investor Reporting to $targetServerName']"
