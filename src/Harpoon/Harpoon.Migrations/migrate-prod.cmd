@echo off

set CONNECTION_STRING=harpoon-prod
set PROFILE=Production

Migrate --assembly="bin\Harpoon.Migrations.dll" --provider="sqlserver" --connectionString=%CONNECTION_STRING% --verbose=true --output --outputFilename="bin\migrated.sql" 

pause
