echo Executing: "$0"

cd ../../tests/

ls | grep -vI Lanre.Tests.Core | while read folder; do echo $folder; dotnet test $folder -P:CollectCoverage=true; done
