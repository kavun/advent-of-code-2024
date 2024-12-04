param(
  [Parameter(Position=0, Mandatory=$True)]
  [ValidateSet("run", "test")]
  [string]$Command
)

function Invoke-Run() {
    & dotnet run --project "$PSScriptRoot/src/adventofcode2024.console/adventofcode2024.console.csproj"
}

function Invoke-Test() {
    & dotnet test
}

switch ($Command) {
    "run"    { Invoke-Run }
    "test"  { Invoke-Test }
}
