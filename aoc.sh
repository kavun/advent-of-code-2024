#!/usr/bin/env bash

set -eo pipefail

script_root="$(dirname "$0")"

echoerr() {
    printf "%s\n" "$*" >&2
}

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
    dotnet run \
        -c Debug \
        --project "${script_root}/src/adventofcode2024.console/adventofcode2024.console.csproj"
}

invoke_test() {
    dotnet run \
        -c Debug \
        --project "${script_root}/tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj" \
        --disable-logo
}

invoke_create_from_template() {
    local template_path="$1"
    local day="$2"
    local target_path
    
    target_path="$(echo "${template_path}" | sed "s/Day00/Day${day}/")"
    
    if [[ -e "${target_path}" ]]; then
        echo "${target_path} already exists."
        return
    fi
    
    if [[ ! -e "${template_path}" ]]; then
        echoerr "${template_path} does not exist."
        exit 1
    fi
    
    cp "${template_path}" "${target_path}"
    sed -i "s/Day00/Day${day}/g" "${target_path}"
    echo "Created ${target_path}"
}

invoke_new_day() {
    if ! [[ "$1" =~ ^[0-9]+$ ]]; then
        echoerr "The day argument must be a positive integer."
        exit 1
    fi
    
    local day="$(printf "%02d" "$1")"
    local input_path="${script_root}/src/adventofcode2024.console/input/Day00.txt"
    local day_path="${script_root}/src/adventofcode2024/Day00.cs"
    local test_path="${script_root}/tests/adventofcode2024.UnitTests/Day00Tests.cs"
    
    invoke_create_from_template "${input_path}" "${day}"
    invoke_create_from_template "${day_path}" "${day}"
    invoke_create_from_template "${test_path}" "${day}"
}

command="${1:-}"
shift || true

case "${command}" in
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
        echoerr "Invalid or missing command. See usage below."
        usage
        exit 1
    ;;
esac