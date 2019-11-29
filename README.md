Abaixo segue o que utilizei para fazer esse projeto:
- Injeção de dependencias nativa do framework .net core. 
- JWT para autenticação na API 
- Banco de dados MySql
- Entity Framework Code First
- Swagger para exibição clara dos contratos 
- Arquitetura em DDD.
- Biblioteca MediatR para cuidar das instancias dos handlers


Configuração do Banco de Dados My Sql:

- Para criação do banco em MySql verificar a string conexão localizada no appsettings.json
- rodar os comandos abaixo no Package Manager Console substituindo {caminho_da_aplicacao} pelo caminho que o projeto foi salvo na maquina:

- dotnet ef migrations add InitialCreate --project "{caminho_da_aplicacao}\DesafioVerityApi\DesafioVerity.Repository\DesafioVerity.Repository.csproj" --startup-project "{caminho_da_aplicacao}\DesafioVerityApi\DesafioVerity.API\DesafioVerity.API.csproj"
- dotnet ef database update --project "{caminho_da_aplicacao}\DesafioVerityApi\DesafioVerity.Repository\DesafioVerity.Repository.csproj" --startup-project "{caminho_da_aplicacao}\DesafioVerityApi\DesafioVerity.API\DesafioVerity.API.csproj"

Swagger:

http://localhost:54633/swagger

Usuário para logar e efetuar testes:

{
	"Agency": 1,
	"AccountNumber": 1234,
	"Password":123456
}
