$env:PYTHONWARNINGS = "ignore"

$kspDirectory = $env:KSP_DIR
if(!$kspDirectory) {
	$kspDirectory = "C:/Program Files (x86)/Kerbal Space Program Dev"
}
$krpcDirectory = Join-Path $kspDirectory "GameData/kRPC"

function BuildDefinitions() {
	Echo "Building definitions"
	$null = New-Item -ItemType Directory -Force -Path "output"

	# The krpctools 0.7.0 package ships a Linux launcher. Run its .NET DLL on Windows.
	$serviceDefinitions = python -c "from importlib.resources import files; print(files('krpctools').joinpath('bin', 'ServiceDefinitions.dll'))"
	if($LASTEXITCODE -ne 0) {
		throw "Could not locate the kRPC service definition tool."
	}
	$managedDirectory = Join-Path $kspDirectory "KSP_x64_Data/Managed"
	dotnet $serviceDefinitions "--reference-dir=$managedDirectory" "--reference-dir=$krpcDirectory" -o output/KRPC.MechJeb.json MechJeb ../bin/Release/KRPC.MechJeb.dll "$krpcDirectory/KRPC.SpaceCenter.dll"
	if($LASTEXITCODE -ne 0) {
		throw "Could not generate the MechJeb service definition."
	}
}

function BuildClients() {
	$extensions = @{
		cnano = "h"
		cpp = "hpp"
		csharp = "cs"
		java = "java"
	}
	
	$names = @{
		cnano = "C-nano"
		cpp = "C++"
		csharp = "C#"
		java = "Java"
	}

	$includes = @{
		cnano = "SpaceCenter=krpc_cnano/services/space_center.h"
		cpp = "SpaceCenter=krpc/services/space_center.hpp"
	}
	
	BuildDefinitions
	
	$clientsDir = "output/clients/"
	if(Test-Path $clientsDir) {
		Echo "Removing clients directory"
		rmdir $clientsDir -recurse
	}
	
	Echo "Building clients"
	foreach($key in $extensions.keys) {
		$log = "  Language: " + $key
		Echo $log
		
		$directory = $clientsDir + $names[$key]
		$null = New-Item -ItemType Directory -Force -Path $directory
		
		$value = $directory + "/MechJeb." + $extensions[$key]
		$includeArguments = @()
		if($includes.ContainsKey($key)) {
			$includeArguments = @("--include", $includes[$key])
		}
		krpc-clientgen $key MechJeb output/KRPC.MechJeb.json "$krpcDirectory/KRPC.SpaceCenter.json" @includeArguments -o $value
		if($LASTEXITCODE -ne 0) {
			throw "Could not generate the $($names[$key]) client binding."
		}
	}
	Copy-Item output/KRPC.MechJeb.json output/clients
}

function CopyDocumentation() {
	$docsPath = "output/docs"
	if(Test-Path $docsPath) {
		Echo "Removing docs directory"
		rmdir $docsPath -recurse
	}
	
	Echo "Copying documentation files"
	robocopy ../docs $docsPath /e > $null
}

function BuildTemplates($languages) {
	CopyDocumentation
	
	Echo "Building templates"
	$files = Get-ChildItem -Path output/docs/* -Include *.tmpl
	foreach($lang in $languages) {
		$log = "  Language: " + $lang
		Echo $log
		
		foreach($file in $files) {
			$log = "    Template: " + $file.BaseName
			Echo $log
			$input = "output/docs/" + $file.BaseName + ".tmpl"
			$output = "output/docs/" + $lang + "/" + $file.BaseName + ".rst"
			krpc-docgen $lang $input ../docs/order.txt $output output/KRPC.MechJeb.json
		}
	}
	
	BuildHTML
}

function BuildAllTemplates() {
	BuildTemplates "cnano","cpp","csharp","java","lua","python"
}

function BuildHTML() {
	Echo "Replacing tabs in:"
	$files = Get-ChildItem -Path output/docs/scripts -Recurse
	foreach($file in $files) {
		if(Test-Path -Path $file.FullName -PathType Leaf) {
			$log = "  " + $file.Name
			Echo $log
			(Get-Content $file.FullName).replace("`t", "    ") | Set-Content $file.FullName
		}
	}
	
	$docsPath = "output/docs"
	# Copy conf.py again if this is called as a standalone function.
	robocopy ../docs $docsPath conf.py > $null

	$htmlPath = "output/html"
	if(Test-Path $htmlPath) {
		Echo "Removing html directory"
		rmdir $htmlPath -recurse
	}
	Echo "Building HTML"
	Echo "=================================================="
	sphinx-build -b html $docsPath $htmlPath
}

function BuildRelease() {
	BuildClients
	
	Echo "Building release archive"
	$source = "output/clients"
	Copy-Item ../bin/Release/KRPC.MechJeb.dll $source
	Copy-Item ../bin/Release/KRPC.MechJeb.xml $source
	Copy-Item ../CHANGELOG.md $source
	Copy-Item ../LICENSE $source
	Copy-Item ../README.md $source
	
	$version = Select-String -Path ../Properties/AssemblyInfo.cs -Pattern 'AssemblyVersion\("([0-9.]+)"\)' | % { $_.Matches.Groups[1].Value }
	$destination = "output/krpc-mechjeb-" + $version + ".zip"
	Get-ChildItem -Path $source | Compress-Archive -Force -DestinationPath $destination
}

Export-ModuleMember -Function "*"
