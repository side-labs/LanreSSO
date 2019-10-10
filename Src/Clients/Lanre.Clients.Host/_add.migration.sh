
echo Executing: "$0"

migration=$1
context="Lanre.Data.Contexts.Lanre.LanreContext"
project="../../Data/Lanre.Data/"
migrationsPath="Contexts/Lanre/Migrations"

echo parameters... migration: $migration, context: $context, project: $project, migrationsPath: $migrationsPath

dotnet ef migrations add $migration -c $context -o $migrationsPath -p $project