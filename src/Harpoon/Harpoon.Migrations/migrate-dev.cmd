@echo off

set CONNECTION_STRING=harpoon-dev
set PROFILE=Development

Migrate --assembly="bin\Harpoon.Migrations.dll" --provider="sqlserver" --connectionString=%CONNECTION_STRING% --verbose=true --output --outputFilename="bin\migrated.sql" 

pause
