
echo Executing: "$0"

concreteServices=$1

echo parameters... concreteServices: $concreteServices

docker-compose -f docker-compose-docs.yml -f docker-compose-docs.override.yml build  $concreteServices && docker-compose -f docker-compose-docs.yml -f docker-compose-docs.override.yml up $concreteServices