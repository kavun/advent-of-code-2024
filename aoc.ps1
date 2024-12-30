param(
    [Parameter(Position = 0, Mandatory = $True)]
    [ValidateSet('run', 'test', 'new')]
    [string]$Command,

    [Parameter(Position = 1, ValueFromRemainingArguments = $true)]
    $Rest
)

function Invoke-Run() {
    & dotnet run -c Debug --project "$PSScriptRoot/src/adventofcode2024.console/adventofcode2024.console.csproj"
}

function Invoke-Test() {
    & dotnet run -c Debug --project "$PSScriptRoot/tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj" --disable-logo
}

function Invoke-CreateFromTemplate($TemplatePath, $Day) {
    $TargetPath = $TemplatePath -replace 'Day00', "Day$Day"

    if (Test-Path $TargetPath) {
        Write-Host "$TargetPath already exists"
    }
    else {
        if (-not (Test-Path $TemplatePath)) {
            Write-Host "$TemplatePath does not exist"
            return
        }

        Copy-Item $TemplatePath $TargetPath
        Write-Host "Created $TargetPath"

        (Get-Content $TargetPath) `
        | ForEach-Object { $_ -replace 'Day00', "Day$Day" } `
        | Set-Content $TargetPath
    }
}

function Invoke-NewDay($Day) {
    if (!($Day -match '^[0-9]+$')) {
        Write-Host "day must be an int"
        return
    }

    $Day = $Day.ToString().Trim().TrimStart('0')
    $Day = $Day.PadLeft(2, '0')

    $InputPath = "$PSScriptRoot/src/adventofcode2024.console/input/Day00.txt"
    $DayPath = "$PSScriptRoot/src/adventofcode2024/Day00.cs"
    $TestPath = "$PSScriptRoot/tests/adventofcode2024.UnitTests/Day00Tests.cs"

    Invoke-CreateFromTemplate $InputPath $Day
    Invoke-CreateFromTemplate $DayPath $Day
    Invoke-CreateFromTemplate $TestPath $Day

    Invoke-Test
}

switch ($Command) {
    'run' { Invoke-Run }
    'test' { Invoke-Test }
    'new' { Invoke-Expression "Invoke-NewDay $Rest" }
}
