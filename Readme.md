# Базовые сущности Кubernetes: Service, Ingress

## ДЗ 15 Apigateway

Запросы идут в один сервис, но авторизация на отдельный контроллер происходит в nginx, 
манифест в файле ингресса otus-auth-ingress.yaml:
```shell
  annotations:
    nginx.ingress.kubernetes.io/auth-url: http://otus-auth-service.default.svc.cluster.local/auth/nginx
```
Ставим nginx:
```shell
helm install nginx ingress-nginx/ingress-nginx -f .\configs\nginx-ingress.yaml
```
Ставим PostgreSQL
```shell
helm install my-release --set auth.postgresPassword=Test123 --set auth.database=otususers bitnami/postgresql
```
Пробросить порт PostgreSQL для проверок:
```shell
 kubectl port-forward --namespace default svc/my-release-postgresql 5432:5432
```
Ставим приложение Auth и Job миграций (создает таблицу и тестовых пользователей):
```shell
kubectl apply -f .\auth
```
Swagger:
```shell
http://arch.homework/swagger/index.html
```
Установка newman для запуска тестов postman:
```shell
npm install -g newman
```
Запуск тестов из каталога OtusCrud\postman:
```shell
newman run .\Otus.Auth.postman_collection.json
```

<details open>
<summary>Результат выполнения</summary>

```shell
Otus.Auth

→ Health
  GET http://arch.homework/health [200 OK, 170B, 26ms]
  √  service are ok

→ Register first user
  POST http://arch.homework/api/v1/auth/register [200 OK, 157B, 9ms]
  √  request is valid JSON
  √  response json contains login

→ First user info
  GET http://arch.homework/api/v1/account [401 Unauthorized, 334B, 4ms]
  √  first user unauthorized to get info

→ First user update info
  PUT http://arch.homework/api/v1/account [401 Unauthorized, 334B, 4ms]
  √  first user unauthorized to update

→ Login first user
  POST http://arch.homework/api/v1/auth/token [200 OK, 554B, 5ms]
  ┌
  │ 'User name: Lydia.Torphy80'
  └
  √  first user login
  √  request is valid JSON

→ First user update info after login
  PUT http://arch.homework/api/v1/account [200 OK, 99B, 8ms]
  √  first user updated

→ First user info after update
  GET http://arch.homework/api/v1/account [200 OK, 282B, 5ms]
  √  first user updated info is ok

→ Register second user
  POST http://arch.homework/api/v1/auth/register [200 OK, 157B, 7ms]
  √  request is valid JSON
  √  response json contains login

→ Login secound user
  POST http://arch.homework/api/v1/auth/token [200 OK, 557B, 4ms]
  ┌
  │ 'User name: Clementina.Kris'
  └
  √  first user login
  √  request is valid JSON

→ Second user info
  GET http://arch.homework/api/v1/account [200 OK, 244B, 5ms]
  √  second user info is ok

┌─────────────────────────┬─────────────────┬─────────────────┐
│                         │        executed │          failed │
├─────────────────────────┼─────────────────┼─────────────────┤
│              iterations │               1 │               0 │
├─────────────────────────┼─────────────────┼─────────────────┤
│                requests │              10 │               0 │
├─────────────────────────┼─────────────────┼─────────────────┤
│            test-scripts │              20 │               0 │
├─────────────────────────┼─────────────────┼─────────────────┤
│      prerequest-scripts │              10 │               0 │
├─────────────────────────┼─────────────────┼─────────────────┤
│              assertions │              14 │               0 │
├─────────────────────────┴─────────────────┴─────────────────┤
│ total run duration: 920ms                                   │
├─────────────────────────────────────────────────────────────┤
│ total data received: 1.39kB (approx)                        │
├─────────────────────────────────────────────────────────────┤
│ average response time: 7ms [min: 4ms, max: 26ms, s.d.: 6ms] │
└─────────────────────────────────────────────────────────────┘
```
</details>

## ДЗ Старое

### Тестирование нагрузки

    .\ab.exe -n 100000 -c 2 http://arch.homework/api/v1/user
    .\ab.exe -n 100000 -c 2 http://arch.homework/api/v1/user/3

### Экспортированный dashboard Grafana c панелями nginx и сервиса

    .\kubernetes\grafana-dashboard.json

### Изменен конфигурационный файл для nginx-ingress.yaml

Включены метрики nginx:
```  
metrics:
    enabled: true
    serviceMonitor:
      enabled: true
```

### Установка Postgresql в k8s
    helm repo add bitnami https://charts.bitnami.com/bitnami

    helm install my-release --set auth.postgresPassword=Test123 --set auth.database=otususers bitnami/postgresql

### Команды k8s

Общий запуск манифестов:

    kubectl apply -f .\kubernetes\manifests\

В нем:

Подключение к БД прописывается в Secrets k8s:

    kubectl apply -f .\otus-crud-secrets.yaml

Первоначальные миграции с наполнением БД запускаются с помощью Job k8s:

    kubectl apply -f .\otus-crud-migrations-job.yaml

### Проверка работы:

    http://arch.homework/health

Swagger:

    http://arch.homework/swagger/index.html

Коллекция postman:

    ./postman/OtusCrud.postman_collection.json

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

### Установка через helm nginx-ingress daemon:

    helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx/
    helm repo update
    helm install nginx ingress-nginx/ingress-nginx -f nginx-ingress.yaml

Обновление конфигурации:

    helm upgrade nginx ingress-nginx/ingress-nginx -f .\nginx-ingress.yaml

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
    
    kubectl apply -f .\kubernetes\

## Добавление Prometheus stack

    helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
    helm repo update
    helm install stack prometheus-community/kube-prometheus-stack -f prometheus.yaml

Пробросить порт для доступа с рабочей машины в prometheus и grafana:

    kubectl port-forward --namespace default svc/prometheus-operated 9090:9090
    kubectl port-forward --namespace default svc/stack-grafana 9000:80

Логин/пароль по умолчанию grafana: admin/prom-operator

## Описание Apache Benchmark для тестирования
https://httpd.apache.org/docs/2.4/programs/ab.html

    .\ab.exe -n 10000 -c 10 http://arch.homework/api/v1/user

Пример использование ab с post запросом и аутентификацией:

    .\ab -p post_loc.txt -T application/json -H 'Authorization: Token abcd1234' -c 10 -n 2000 http://example.com/api/v1/locations/
