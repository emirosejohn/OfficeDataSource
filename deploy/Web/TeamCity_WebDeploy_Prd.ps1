param(
	[String]$ProjectName = 'OfficeLocationMicroservice',           #name of project
    [String]$targetServerName = '***REMOVED***'                    #machine name where we are connecting to
)
cls

$baseDir = (resolve-path .)
$teamCityFileLocation = "$baseDir\temp\$ProjectName"               #where the files in the team city agent are located
$remoteServerPath = '\\' + $targetServerName + '\c$\temp\' + $ProjectName + '\'

If (Test-Path "$remoteServerPath") {
	Write-Host "Deleting contents: $remoteServerPath"
	Remove-Item "$remoteServerPath\*" -recurse -Force
}
Else {
	Write-Host "Creating folder: $remoteServerPath"
	New-Item -ItemType directory -Path $remoteServerPath
}

#copies from team city agent into temporary working directory
Write-Host "copying from $teamCityFileLocation\* to  $remoteServerPath"
Copy-Item "$teamCityFileLocation\*" $remoteServerPath -recurse

$sess = New-PSSession -ComputerName $targetServerName 
write-host "##teamcity[progressStart 'Install of Office Location Microservice to $targetServerName']"


Invoke-Command -Session $sess -ArgumentList ($ProjectName)  -Scriptblock { 
    $ProjectName = $($args[0])

	#cd "c:\temp\$ProjectName" include to use the artifact scripts instead.

	.\deploy\Web\Local_WebDeploy_Prd.ps1 -projectName $ProjectName

	Write-Host "Build exit code:" $LastExitCode

	# Propagating the exit code so that builds actually fail when there is a problem
	exit $LastExitCode
}

write-host "##teamcity[progressStart 'Done with remote install of Office Location Microservice to $targetServerName']"

