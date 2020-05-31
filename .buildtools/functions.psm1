function BuildDefinitions() {
	Echo "Building definitions"
	$null = New-Item -ItemType Directory -Force -Path "output"
	krpc-servicedefs "C:/Steam/steamapps/common/Kerbal Space Program" MechJeb ../bin/Release/KRPC.MechJeb.dll ../lib/KRPC.SpaceCenter.dll -o output/KRPC.MechJeb.json
}

function BuildClients() {
	$extensions = @{
		cnano = "c"
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
		krpc-clientgen $key MechJeb output/KRPC.MechJeb.json -o $value
	}
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
	
	$htmlPath = "output/html"
	if(Test-Path $htmlPath) {
		Echo "Removing html directory"
		rmdir $htmlPath -recurse
	}
	Echo "Building HTML"
	Echo "=================================================="
	sphinx-build -b html output/docs $htmlPath
}

function BuildRelease() {
	BuildClients
	
	Echo "Building release archive"
	$source = "output/clients"
	Copy-Item ../bin/Release/KRPC.MechJeb.dll $source
	Copy-Item ../CHANGELOG.md $source
	Copy-Item ../LICENSE $source
	Copy-Item ../README.md $source
	
	$version = Select-String -Path ../Properties/AssemblyInfo.cs -Pattern 'AssemblyVersion\("([0-9.]+)"\)' | % { $_.Matches.Groups[1].Value }
	$destination = "output/krpc-mechjeb-" + $version + ".zip"
	Get-ChildItem -Path $source | Compress-Archive -Force -DestinationPath $destination
}

Export-ModuleMember -Function "*"
