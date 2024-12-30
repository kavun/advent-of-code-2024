#!/usr/bin/env bash

set -eo pipefail
trap 'echo Error at line $LINENO' ERR

script_root="$(dirname "$0")"

usage() {
    echo "Usage: $(basename "$0") <command> [args]"
    echo
    echo "Commands:"
    echo "  run             Run the main console application."
    echo "  test            Run the unit tests."
    echo "  new <day>       Create a new day with template files."
    echo
    echo "Examples:"
    echo "  $(basename "$0") run"
    echo "  $(basename "$0") test"
    echo "  $(basename "$0") new 5"
}

invoke_run() {
    set -x
    dotnet run -c Debug --project "$script_root/src/adventofcode2024.console/adventofcode2024.console.csproj"
}

invoke_test() {
    set -x
    dotnet run -c Debug --project "$script_root/tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj" --disable-logo
}

invoke_create() {
    local template="$1"
    if [[ ! -e "$template" ]]; then
        echo "$template does not exist."
        exit 1
    fi
    
    local day="$2"
    local target="$(echo "$template" | sed "s/Day00/Day$day/")"
    if [[ -e "$target" ]]; then
        echo "$target already exists."
        return
    fi
    
    cp "$template" "$target"
    sed -i "s/Day00/Day$day/g" "$target"
    echo "Created $target"
}

invoke_new_day() {
    if [[ ! "$1" =~ ^[0-9]+$ ]]; then
        echo "day must be an int"
        exit 1
    fi
    
    local day="$(printf '%02d' "$1")"
    local input_path="$script_root/src/adventofcode2024.console/input/Day00.txt"
    local day_path="$script_root/src/adventofcode2024/Day00.cs"
    local test_path="$script_root/tests/adventofcode2024.UnitTests/Day00Tests.cs"
    
    invoke_create "$input_path" "$day"
    invoke_create "$day_path" "$day"
    invoke_create "$test_path" "$day"
}

command="${1:-}"
shift || true

case "$command" in
    run)
        invoke_run
    ;;
    test)
        invoke_test
    ;;
    new)
        invoke_new_day "$1"
    ;;
    *)
        usage
        exit 1
    ;;
esac
