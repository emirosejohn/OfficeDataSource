include "configfile.ps1"

properties {
	$baseDir = resolve-path .
    $siteLocation = "C:\UtilityApps\$ProjectName"

    $dataFolder = "$sitelocation\data"
    $roundhouseExec = "$siteLocation\lib\roundhouse\rh.exe"
	
    $databaseName = $projectName
    $dbFileDir = "$dataFolder\mssql\$ProjectName"
    $versionFile = "$dbFileDir\_BuildInfo.xml"

    $tempLocation = "C:\temp\$ProjectName"

    $webConfigFile = "$tempLocation\Release\_PublishedWebsites\WebUI\Web.config"
}

task default -depends ConfigureWeb, CopyTempToSiteLocation

formatTaskName {
	param($taskName)
	write-host "********************** $taskName **********************" -ForegroundColor Cyan
}


task ConfigureWeb -requiredVariables enviornment, CountryWebApiUrl {

ChangeConnectionString $webConfigFile "OfficeLocationDatabase" (OfficeLocationMicroserviceConnectionString "$enviornment")
ChangeAppSetting $webConfigFile "CountryWebApiUrl" $CountryWebApiUrl

}


task CopyTempToSiteLocation -depends ConfigureWeb{

    rm "$tempLocation\*.zip" #removes artifact zip.  Should have files already unpacked by team city.

    If (Test-Path "$siteLocation") {
	    Write-Host "Deleting contents: $siteLocation"
	    Get-ChildItem $siteLocation -Recurse -Force | Remove-Item -Force  
    }
	    Write-Host "Creating folder: $siteLocation"
	    New-Item -ItemType directory -Path $siteLocation
    

    #Move from temp to the site folder.
    Write-Host "copying from $tempLocation\* to  $siteLocation"
    Copy-Item "$tempLocation\*" $siteLocation -recurse -Force

}
 
task RebuildDatabase{

    #databaseServer and environment are both passed in.
    &$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent

}



# ***********

Function OfficeLocationMicroserviceConnectionString([string] $env)
{
	return "Initial Catalog=Characteristic;Data Source=OfficeLocationMicroserviceDB-$env;Integrated Security=SSPI;"
}


Function ChangeLinkedServerReference([string] $filePath, [string] $env)
{
	Write-Host "Changing link server references in file $filePath to $env"

    (Get-Content $filePath) `
        | ForEach-object {$_ -replace "-PRD", "-$env" } `
        | ForEach-object {$_ -replace "-STG", "-$env" } `
        | ForEach-object {$_ -replace "-DEV", "-$env" } `
        | Set-Content $filePath
}