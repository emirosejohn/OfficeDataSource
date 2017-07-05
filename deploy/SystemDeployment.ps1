﻿GET-PSSession | Remove-PSSession

$ProjectName = 'OfficeLocationMicroservice'           #name of project
$baseDir = (resolve-path .)

Write-Host "Base Directory: $baseDir"
$baseDir = "$baseDir\temp\$ProjectName"  #where the files in the team city agent are located
Write-Host "Team city artifact dependency location: $baseDir"

$targetServerName = '***REMOVED***'   #machine name where we are connecting to

$sess = New-PSSession -ComputerName $targetServerName 

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
Write-Host "copying from $baseDir\* to  $remoteServerPath"
Copy-Item "$baseDir\*" $remoteServerPath -recurse


Invoke-Command -Session $sess -ArgumentList ($ProjectName)  -Scriptblock {
$ProjectName = $($args[0])

$siteLocation = "C:\UtilityApps\$ProjectName"

$dataFolder = "$sitelocation\data"
$roundhouseExec = "$siteLocation\lib\roundhouse\rh.exe"
$databaseName = $projectName

$databaseServer = "(local)\sqlexpress"

$dbFileDir = "$dataFolder\mssql\$ProjectName"
$versionFile = "$dbFileDir\_BuildInfo.xml"
$enviornment = "LOCAL"



rm "C:\temp\$ProjectName\*.zip" #removes artifact zip.  Should have files already unpacked by team city.

If (Test-Path "$siteLocation") {
	Write-Host "Deleting contents: $siteLocation"
	Remove-Item "$siteLocation\*" -recurse -Force
}

Else {
	Write-Host "Creating folder: $siteLocation"
	New-Item -ItemType directory -Path $siteLocation
}

#Move from temp to the site folder.
Write-Host "copying from C:\temp\$ProjectName\* to  $siteLocation"
Copy-Item "C:\temp\$ProjectName\*" $siteLocation -recurse -Force


#rebuild databases

#&$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent

#Set up Site using iis
Import-Module WebAdministration


$directoryPath = "$siteLocation\Release\_PublishedWebsites\WebUI" #path where the website is at

cd IIS:\Sites\

if (Test-Path $ProjectName -pathType container)
{
    Write-Host "Removing IIS:\Sites\$ProjectName"
    Remove-Item "$ProjectName" -recurse -Force
}

Write-Host "making item at IIS:\Sites\$ProjectName"
$iisApp = New-Item $ProjectName -bindings @{protocol="http";bindingInformation=":1704:"} -physicalPath $directoryPath

$iisApp | Set-ItemProperty -Name "applicationPool" -Value "$ProjectName"

}

#Enter-PSSession $sess
