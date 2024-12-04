param(
    [Parameter(Position = 0, Mandatory = $True)]
    [ValidateSet('run', 'test')]
    [string]$Command
)

function Invoke-Run() {
    & dotnet run -c Release --project "$PSScriptRoot/src/adventofcode2024.console/adventofcode2024.console.csproj"
}

function Invoke-Test() {
    & dotnet run -c Release --project "$PSScriptRoot/tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj"
}

switch ($Command) {
    'run' { Invoke-Run }
    'test' { Invoke-Test }
}
