# TesteEstagioASP.NET
API desevolvida para como teste para vaga de estágio em C#

## Documentação
A documentação dessa API é feita através do Swagger.
Para acessar a documentação, acesse a rota /swagger.

# Token JWT obrigatório
User o Token em todas requisições execto na rota /api/usuario nos metodos [GET] e [POST]

## Como Gerar Token
Para gerar o Token e necessário fazer uma requisição do tipo [POST] para 
a rota /api/usuario/create contendo:
```
{
	"Email": "Seu e-mail",
	"Password": "Sua senha"
}
```

Caso já tenha um usuário: [POST] api/usuario/login
```
{
	"Email": "Seu e-mail",
	"Password": "Sua senha"
}
```