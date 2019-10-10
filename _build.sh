
echo Executing: "$0"

concreteServices=$1

echo parameters... concreteServices: $concreteServices

docker-compose build $concreteServices && docker-compose up $concreteServices