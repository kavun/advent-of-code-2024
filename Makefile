SCRIPT_ROOT := $(dir $(abspath $(lastword $(MAKEFILE_LIST))))

.PHONY: help run test new create

help:
	@echo "Usage: make <target> [variables]"
	@echo
	@echo "Targets:"
	@echo "  run               Run the main console application."
	@echo "  test              Run the unit tests."
	@echo "  new DAY=<num>     Create a new day with template files."
	@echo
	@echo "Examples:"
	@echo "  make run"
	@echo "  make test"
	@echo "  make new DAY=5"

run:
	dotnet run -c Debug --project "$(SCRIPT_ROOT)src/adventofcode2024.console/adventofcode2024.console.csproj"

test:
	dotnet run -c Debug --project "$(SCRIPT_ROOT)tests/adventofcode2024.UnitTests/adventofcode2024.UnitTests.csproj" --disable-logo

new:
	@if [ -z "$(DAY)" ]; then \
	    echo "Error: 'DAY' variable must be set (e.g. make new DAY=5)"; \
	    exit 1; \
	fi

	@expr "$(DAY)" + 1 >/dev/null 2>&1 || { \
	    echo "Error: 'DAY' must be an integer"; \
	    exit 1; \
	}

	$(eval DAY_PAD = $(shell printf "%02d" $(DAY)))

	$(MAKE) create \
		TEMPLATE_PATH="$(SCRIPT_ROOT)src/adventofcode2024.console/input/Day00.txt" \
		DAY="$(DAY_PAD)"

	$(MAKE) create \
		TEMPLATE_PATH="$(SCRIPT_ROOT)src/adventofcode2024/Day00.cs" \
		DAY="$(DAY_PAD)"

	$(MAKE) create \
		TEMPLATE_PATH="$(SCRIPT_ROOT)tests/adventofcode2024.UnitTests/Day00Tests.cs" \
		DAY="$(DAY_PAD)"

create:
	@if [ -z "$(TEMPLATE_PATH)" ]; then \
	    echo "Error: 'TEMPLATE_PATH' must be set"; \
	    exit 1; \
	fi
	@if [ -z "$(DAY)" ]; then \
	    echo "Error: 'DAY' must be set"; \
	    exit 1; \
	fi

	@target_path="$$(echo "$(TEMPLATE_PATH)" | sed "s/Day00/Day$(DAY)/")"; \
	 if [ -e "$$target_path" ]; then \
	    echo "$$target_path already exists."; \
	 else \
	    if [ ! -e "$(TEMPLATE_PATH)" ]; then \
	        echo "Error: Template file '$(TEMPLATE_PATH)' does not exist."; \
	        exit 1; \
	    fi; \
	    cp "$(TEMPLATE_PATH)" "$$target_path"; \
	    sed -i "s/Day00/Day$(DAY)/g" "$$target_path"; \
	    echo "Created $$target_path"; \
	 fi
