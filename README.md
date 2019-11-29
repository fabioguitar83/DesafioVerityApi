<b>Instruções para criação do banco de dados no My SQL</b>

Executar as instruções abaixo:

dotnet ef migrations add InitialCreate --project "C:\Fabio\GitHub\DesafioVerity\DesafioVerity.Repository\DesafioVerity.Repository.csproj" --startup-project "C:\Fabio\GitHub\DesafioVerity\DesafioVerity.API\DesafioVerity.API.csproj"
dotnet ef database update --project "C:\Fabio\GitHub\DesafioVerity\DesafioVerity.Repository\DesafioVerity.Repository.csproj" --startup-project "C:\Fabio\GitHub\DesafioVerity\DesafioVerity.API\DesafioVerity.API.csproj"
