
echo Executing: "$0"

concreteServices=$1

echo parameters... concreteServices: $concreteServices

docker-compose -f docker-compose-tests.yml -f docker-compose-tests.override.yml build  $concreteServices && docker-compose -f docker-compose-tests.yml -f docker-compose-tests.override.yml up $concreteServices