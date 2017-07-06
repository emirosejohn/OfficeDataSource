﻿#$ProjectName = 'OfficeLocationMicroservice'           #name of project
properties {
	$baseDir = resolve-path .
    $siteLocation = "C:\UtilityApps\$ProjectName"

    $dataFolder = "$sitelocation\data"
    $roundhouseExec = "$siteLocation\lib\roundhouse\rh.exe"
	
    $databaseName = $projectName
    $dbFileDir = "$dataFolder\mssql\$ProjectName"
    $versionFile = "$dbFileDir\_BuildInfo.xml"
}

task default -depends CopyTempToSiteLocation, RebuildDatabase, RunIIS


task CopyTempToSiteLocation{

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

}
 
task RebuildDatabase{

    #databaseServer and enviornment are both passed in.
    &$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent

}

task RunIIS{

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