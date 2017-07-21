properties {
	$baseDir = (resolve-path .\..\..)
    $teamCityFileLocation = "$baseDir\temp\$ProjectName"

    $dataFolder = "$teamCityFileLocation\data"


    $roundhouseExec = "$teamCityFileLocation\lib\roundhouse\rh.exe"

    $databaseName = $projectName
    $dbFileDir = "$dataFolder\mssql\$ProjectName"
    $versionFile = "$dbFileDir\_BuildInfo.xml"
}

task default -depends RebuildDatabase

formatTaskName {
	param($taskName)
	write-host "********************** $taskName **********************" -ForegroundColor Green
}

task RebuildDatabase{

    Write-Host .
    #databaseServer and environment are both passed in.
    &$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent

}
