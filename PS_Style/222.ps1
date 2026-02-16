try {
	$input = $args[0]

	$inputPath = $input.Split(' ', 2)[1]

	$fileContent = Get-Content -Path $inputPath -Raw

	$fileBaseName = (Get-Item $inputPath).BaseName
	$fileParentPath = Split-Path -Path $inputPath -Parent

	$contentUpperCase = $fileContent.ToUpper()

	$outputPath = $fileParentPath + "\" + $fileBaseName + "_COMPLETE.txt"

	Set-Content -Path "$outputPath" -Value $contentUpperCase
	Write-Host "Файл обработан."
}
catch {
	exit 1
}
