properties {
	$baseDir = (resolve-path .\..\..)
    $teamCityFileLocation = "$baseDir\temp\$ProjectName"

    $dataFolder = "$baseDir\data"


    $roundhouseExec = "$baseDir\lib\roundhouse\rh.exe"

    $databaseName = $projectName
    $dbFileDir = "$dataFolder\mssql\$ProjectName"
    $versionFile = "$dbFileDir\_BuildInfo.xml"
}

task default -depends RebuildDatabase

formatTaskName {
	param($taskName)
	write-host "********************** $taskName **********************" -ForegroundColor Green
}

task RebuildDatabase -requiredVariables environment {

    Write-Host $baseDir
    Write-Host $roundhouseExec
    Get-ChildItem "$baseDir\lib\roundhouse"

    $exists = (Test-Path "$versionFile")

    Write-Host "$versionFile exists: $exists"
    #databaseServer and environment are both passed in.
    Exec { 
        &$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$environment /simple /silent
    }
}
