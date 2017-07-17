Function ChangeProperty([string] $configPath, [string] $xpath, [string] $newValue) {
	Write-Host "Changing File '$configPath': Setting XPath property '$xpath' to '$newValue'"

	$xml = [xml](get-content $configPath)
	$root = $xml.get_DocumentElement();
	$node = $root.SelectSingleNode($xpath)
	$node.value = $newValue
	$xml.Save($configPath)
}

Function ChangeConnectionString([string] $configPath, [string] $name, [string] $connectionString) {
	Write-Host "Changing File '$configPath': Setting ConnectionString '$name' to '$connectionString'"

	$xml = [xml](get-content $configPath)
	$root = $xml.get_DocumentElement();
	$node = $root.SelectSingleNode("//configuration/connectionStrings/add[@name='$name']/@connectionString")
	$node.value = $connectionString
	$xml.Save($configPath)
}

Function ChangeAppSetting([string] $configPath, [string] $key, [string] $newValue) {
	Write-Host "Changing File '$configPath': Setting Application Setting '$key' to '$newValue'"
	
	$xpath = "//configuration/appSettings/add[@key='$key']/@value"

	$xml = [xml](get-content $configPath)
	$root = $xml.get_DocumentElement();
	$node = $root.SelectSingleNode($xpath)
	$node.value = $newValue
	$xml.Save($configPath)
}

Function AddLog4NetAppender([string] $configPath, [string] $ref)
{
	Write-Host "Changing File '$configPath': Adding Log4Net appender '$ref'"
	
	$xml = [xml](get-content $configPath)
	$root = $xml.get_DocumentElement();
	
	$exisitingNode = $root.SelectSingleNode("//configuration/log4net/root/appender-ref[@ref=`'$ref`']")
		
	if ($exisitingNode -ne $null) {
		Write-Host "Appender already found skipping."
		Return
	}

	$node = $root.SelectSingleNode("//configuration/log4net/root")
	$newAppender = $xml.CreateElement("appender-ref")
	$none1 = $newAppender.SetAttribute("ref", "",  $ref)

	$none2 = $node.AppendChild($newAppender)

	$xml.Save($configPath)
}

Function RemoveLog4NetAppender([string] $configPath, [string] $ref)
{
	Write-Host "Changing File '$configPath': Removing Log4Net appender '$ref'"

	$xml = [xml](get-content $configPath)
	$root = $xml.get_DocumentElement();
	$node = $root.SelectSingleNode("//configuration/log4net/root")

	$appender = $xml.SelectSingleNode("appender-ref[@ref='$ref']")
	$node.RemoveChild($appender)
	
	$xml.Save($configPath)
}