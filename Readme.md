# Базовые сущности Kubernetes

## ДЗ

### Установка Postgresql в k8s
    helm repo add bitnami https://charts.bitnami.com/bitnami

    helm install my-release --set auth.postgresPassword=Test123 --set auth.database=otususers bitnami/postgresql


## Подготовка
Предварительно установлен minikube:

https://kubernetes.io/ru/docs/setup/learning-environment/minikube/

Запускаем minikube:
 
    minikube start --vm-driver=hyperv

IP адрес выхода minikube:
    
    minikube ip

Открываем dashboard:

    minikube dashboard

Создаем namespace с именем testing: 

    kubectl create namespace testing

Установка через helm nginx-ingress daemon:

    helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx/
    helm repo update
    helm install nginx ingress-nginx/ingress-nginx --namespace testing -f nginx-ingress.yaml

## Установка Postgresql в k8s

    helm repo add bitnami https://charts.bitnami.com/bitnami

    helm install my-release --set auth.postgresPassword=Test123 --set auth.database=otususers bitnami/postgresql

** Please be patient while the chart is being deployed **

PostgreSQL can be accessed via port 5432 on the following DNS names from within your cluster:

    my-release-postgresql.default.svc.cluster.local - Read/Write connection

To get the password for "postgres" run:

    export POSTGRES_PASSWORD=$(kubectl get secret --namespace default my-release-postgresql -o jsonpath="{.data.postgres-password}" | base64 -d)

To connect to your database run the following command:

    kubectl run my-release-postgresql-client --rm --tty -i --restart='Never' --namespace default --image docker.io/bitnami/postgresql:15.1.0-debian-11-r0 --env="PGPASSWORD=$POSTGRES_PASSWORD" \
      --command -- psql --host my-release-postgresql -U postgres -d otususers -p 5432

    > NOTE: If you access the container using bash, make sure that you execute "/opt/bitnami/scripts/postgresql/entrypoint.sh /bin/bash" in order to avoid the error "psql: local user with ID 1001} does not exist"

To connect to your database from outside the cluster execute the following commands:

    kubectl port-forward --namespace default svc/my-release-postgresql 5432:5432 &
    PGPASSWORD="$POSTGRES_PASSWORD" psql --host 127.0.0.1 -U postgres -d otususers -p 5432

Пробросить порт для доступа с рабочей машины (тест приложения):
    
    kubectl port-forward --namespace default svc/my-release-postgresql 5432:5432

Для доступа из контейнера необходимо в коннект к БД прописать вместо localhost:

    Server=host.docker.internal

## Добавление конфигураций
    kubectl apply -f .\otus-docker-deployment.yaml --namespace=testing
    kubectl apply -f .\otus-docker-service.yaml --namespace=testing
    kubectl apply -f .\otus-docker-ingress.yaml --namespace=testing

