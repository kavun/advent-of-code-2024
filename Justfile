default:
    just --list

# Run the main console application.
run:
    dotnet run -c Debug --project "./src/adventofcode2024.console/adventofcode2024.console.csproj"

# Run the unit tests.
test:
    dotnet run -c Debug --project "./tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj" --disable-logo

# Create a new day from template files.
new day:
    #!/usr/bin/env bash
    set -euo pipefail

    if [[ ! "{{day}}" =~ ^[0-9]+$ ]]; then echo "day must an int"; exit 1; fi

    day_formatted=$(printf "%02d" "{{day}}")
    input_path="./src/adventofcode2024.console/input/Day00.txt"
    day_path="./src/adventofcode2024/Day00.cs"
    test_path="./tests/adventofcode2024.UnitTests/Day00Tests.cs"

    just --one _create $input_path $day_formatted
    just --one _create $day_path $day_formatted
    just --one _create $test_path $day_formatted

# Create a template file for a day
_create template day:
    #!/usr/bin/env bash
    set -euo pipefail

    if [[ ! -e "{{template}}" ]]; then
        echo ""{{template}}" does not exist."
        exit 1
    fi
    
    target_path=$(echo "{{template}}" | sed "s/Day00/Day{{day}}/")
    
    if [[ -e $target_path ]]; then
        echo "$target_path already exists."
    else
        cp "{{template}}" $target_path
        sed -i "s/Day00/Day{{day}}/g" $target_path
        echo "Created $target_path"
    fi
    