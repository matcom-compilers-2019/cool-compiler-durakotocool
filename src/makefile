.PHONY: build restore clean main 

CMP ?= test

main:| clean restore build
	
clean:
	dotnet clean Cool/ 
 
restore:
	dotnet restore Cool/
 
build:
	dotnet build Cool/
 
full-process:
	dotnet Cool/bin/Debug/netcoreapp2.0/Cool.dll $(CMP).cl
	#spim -f $(CMP).mips
	java -jar mars.jar $(CMP).mips