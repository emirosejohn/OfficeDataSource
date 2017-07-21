properties {
	$projectName = "OfficeLocationMicroservice"
	$baseDir = resolve-path "."
	$buildConfig = "Release"
	$databaseChangeOwner = "Paul Herrera"

	$buildFolder = "$baseDir\.build"
	$srcFolder = "$baseDir\src"
	$docsFolder = "$baseDir\docs"
	$dataFolder = "$baseDir\data"
    $deployFolder = "$baseDir\deploy"
	
	$buildTargetFolder = "$buildFolder\$buildConfig"

	$buildLibFolder = "$buildFolder\lib"
	$buildDataFolder = "$buildFolder\data"
	$buildWebFolder = "$buildFolder\Web"
    $buildDeployFolder = "$buildFolder\deploy"

	$databaseServer = "(local)\sqlexpress"
	$databaseName = $projectName

#	$standardDatabaseObjectsFolder = "$baseDir\data\mssql\StandardObjects"
#	$databaseScripts = "$baseDir\data\mssql\TinyReturns\TransitionsScripts"

#	$dbdeployExec = "$baseDir\lib\dbdeploy\dbdeploy.exe"
	$roundhouseExec = "$srcFolder\packages\roundhouse.0.8.6\bin\rh.exe"

#	$doDatabaseScriptPath = "$buildFolder\DatabaseUpgrade_GIPS_Local_$dateStamp.sql"
#	$undoDatabaseScriptPath = "$buildFolder\DatabaseRollback_GIPS_Local_$dateStamp.sql"
	
	$solutionFile = "$srcFolder\OfficeLocationMicroservice.sln"

	$packageXunitExec = "$srcFolder\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe"
}

task default -depends CleanSolution, UpdateNuGetPackages, BuildSolution, RebuildDatabase,`
 RunUnitTests, RunIntegrationTests

task databaseonly -depends RebuildDatabase

 task teamcity -depends CleanSolution, UpdateNuGetPackages, BuildSolution, `
 RebuildDatabase, RunUnitTests, RunIntegrationTests, RebuildDatabase,  ZipFile

formatTaskName {
	param($taskName)
	write-host "********************** $taskName **********************" -foregroundcolor Green
}

task CleanSolution {
	if (Test-Path $buildFolder) {
		rd $buildFolder -rec -force | out-null
	}

	mkdir $buildFolder | out-null

	Exec { msbuild "$solutionFile" /t:Clean /p:Configuration=$buildConfig /v:quiet }
}

task UpdateNuGetPackages {
	$nuGetExec = "$baseDir\src\.nuget\NuGet.exe"
	
	&$nuGetExec restore "$solutionFile" -PackagesDirectory "$baseDir\src\packages"
}

task BuildSolution -depends CleanSolution {
	Exec { msbuild "$solutionFile" /t:Build /p:Configuration=Release /v:quiet /p:OutDir="$buildTargetFolder\" }
}

task RunUnitTests -depends BuildSolution {
	Exec { &$packageXunitExec "$buildTargetFolder\OfficeLocationMicroservice.UnitTests.dll" -html "$buildFolder\OfficeLocationMicroservice.UnitTests.dll.html" -parallel none }
}

task RunIntegrationTests -depends BuildSolution {
	Exec { &$packageXunitExec "$buildTargetFolder\OfficeLocationMicroservice.IntegrationTests.dll" -html "$buildFolder\OfficeLocationMicroservice.IntegrationTests.dll.html" -parallel none }
}

task RebuildDatabase {
	$dbFileDir = "$dataFolder\mssql\OfficeLocationMicroservice"
	$versionFile = "$dbFileDir\_BuildInfo.xml"
	$enviornment = "LOCAL"

	Exec {
		&$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /drop /silent
	}

	Exec {
		&$roundhouseExec /d=$databaseName /f=$dbFileDir /s=$databaseServer /vf=$versionFile /vx='//buildInfo/version' /env=$enviornment /simple /silent
	}
}

task copyBuildFiles -depends BuildSolution {
    #buildsolution runs cleansolution, which removes all these directories for us,
    # so we don't have conflicts.
    
    mkdir $buildWebFolder | out-null
	
	$sourceFiles = "$buildTargetFolder\_PublishedWebsites\WebUI\*"
	Write-Host "Copying files from '$sourceFiles' to '$buildWebFolder'"
	copy-item $sourceFiles "$buildWebFolder" -recurse

    mkdir $builddeployFolder | out-null
	
	Write-Host "Copying files from '$deployFolder' to '$buildDeployFolder'"
	copy-item "$deployFolder\*" "$buildDeployFolder" -recurse


    mkdir $buildTargetFolder\_PublishedWebsites\Web\bin\roslyn |out-null

	$roslyn = "$buildTargetFolder\roslyn\"
	Write-Host "Copying files from '$roslyn' to '$buildWebFolder'"
	copy-item $roslyn "$buildTargetFolder\_PublishedWebsites\WebUI\bin\roslyn" -recurse -Force

	mkdir $buildLibFolder | out-null

	$destXunitFolder = "$buildLibFolder\xunit"
	mkdir $destXunitFolder | out-null
	copy-item "$srcFolder\packages\xunit.runner.console.2.2.0\tools\*" $destXunitFolder -recurse
	
    $destRoundhouseFolder = "$buildLibFolder\roundhouse"
	mkdir $destRoundhouseFolder | out-null
	copy-item "$srcFolder\packages\roundhouse.0.8.6\bin\*" $destRoundhouseFolder  -recurse

    $destPsakeFolder = "$buildLibFolder\psake"
	mkdir $destPsakeFolder | out-null
	copy-item "$srcFolder\packages\psake*\tools\*" $destPsakeFolder -recurse

    $destnugetFolder = "$buildLibFolder\nuget"
    mkdir $destnugetFolder | out-null
    copy-item "$baseDir\src\.nuget\*" $destnugetFolder -recurse

	copy-item -Path "$dataFolder" -Destination  "$buildDataFolder" -recurse
}

task ZipFile -depends copyBuildFiles -requiredVariables projectVersion{

    $zipExec = "$srcFolder\packages\7-Zip.CommandLine.9.20.0\tools\7za.exe"

    $versionStamp = $projectVersion -replace "\.", "_"

    Exec { &$zipExec a "-x!*.zip" "-x!*.dat" "$buildFolder\OfficeLocationMicroservice_App_$versionStamp.zip" "$buildFolder\*" }

}