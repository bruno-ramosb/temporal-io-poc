# POC TemporalIO

## What is Temporal?
Temporal is a scalable and reliable runtime for durable function executions called Temporal Workflow Executions.

In other words, it's a platform that guarantees the Durable Execution of your application code. It enables you to develop as if failures don't even exist. Your application will run reliably even if it encounters problems such as network outages or server crashes, which would be catastrophic for a typical application. The Temporal Platform handles these types of problems, allowing you to focus on the business logic instead of writing application code to detect and recover from failures.

For more information, visit the [Temporal documentation](https://docs.temporal.io/temporal).

## Requisitos
É necessário docker e docker-compose para rodar a aplicação.

## Running the Application
Na raiz do projeto executar o comando:
```shell
docker-compose up --build -d
```
A aplicacão fica disponivel na porta http:8080

Exemplo de requisição para o Postman:
```shell
curl --location 'http://localhost:8080/payment' \
--header 'Content-Type: application/json' \
--data-raw '{
  "paymentDetails": "PAYMENT#####",
  "orderId": "OAT132D",
  "email": "email@email.com",
  "ThrowException": true
}'
```
O parametro "ThrowException" serve para simular erro em algum step da atividade para que seja executada do ponto onde gerou o erro em diante.