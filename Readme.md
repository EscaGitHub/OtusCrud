# Базовые сущности Kubernetes

## ДЗ

Манифесты: 

    kubectl apply -f .\kubernetes\manifests\

Вызов сервиса: 
    
    curl http://arch.homework/health

Rewrite:
    
    curl http://arch.homework/otusapp/ilya/health

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

## Добавление конфигураций
    kubectl apply -f .\otus-docker-deployment.yaml --namespace=testing
    kubectl apply -f .\otus-docker-service.yaml --namespace=testing
    kubectl apply -f .\otus-docker-ingress.yaml --namespace=testing

