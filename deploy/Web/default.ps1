properties {
	$baseDir = resolve-path .
    $siteLocation = "C:\UtilityApps\$ProjectName"

    $dataFolder = "$sitelocation\data"
    $roundhouseExec = "$siteLocation\lib\roundhouse\rh.exe"
	
    $databaseName = $projectName
    $dbFileDir = "$dataFolder\mssql\$ProjectName"
    $versionFile = "$dbFileDir\_BuildInfo.xml"
}

task default -depends CopyTempToSiteLocation

formatTaskName {
	param($taskName)
	write-host "********************** $taskName **********************" -ForegroundColor Cyan
}

task CopyTempToSiteLocation{

    rm "C:\temp\$ProjectName\*.zip" #removes artifact zip.  Should have files already unpacked by team city.

    If (Test-Path "$siteLocation") {
	    Write-Host "Deleting contents: $siteLocation"
	    Get-ChildItem "$siteLocation/*" -Recurse -Force | Remove-Item -Force  
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

    #databaseServer and environment are both passed in.
    &$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent

}