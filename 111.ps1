try {
	$input = $args[0]

	$inputPath = $input.Split(' ', 2)[1]

	Get-ChildItem -Path "$inputPath" -Name
}
catch {
	exit 1
}