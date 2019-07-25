# Jurify.Advogados.Api

API RESTFUL que fornece dados referentes ao CRM Jurift para o [front-end](https://github.com/gustavoosantoos/Jurify.Advogados.Web).

### Tecnologias:
- .NET Core 2.2
- PostgreSQL
- Autenticação e Autorização com IdentityServer (se comunica com o [Jurify.Autenticador](https://github.com/gustavoosantoos/Jurify.Autenticador))

### Pré Requisitos:

- Instalar o SDK do .NET Core 2.2: https://dotnet.microsoft.com/download
- Instalar o Visual Studio 2017 ou 2019 (preferencial o 2019) com as ferramentas para .NET Core.
- PostgreSQL e PgAdmin4 para alterações e consultas no banco de dados.

### Como rodar o projeto:

- Configure os pré-requisitos
- Clone o projeto
- Abra a solução no Visual Studio
- Verifique no arquivo appsettings.Development.json:
    - Se a configuração **ConnectionString:Default** aponta para o banco de dados que você deseja (nuvem ou local). Para criar o banco na sua máquina, existe o arquivo **jurify-advogados-ddl.sql** com a estrutura do banco.
    - Se a configuração **Authentication:Authority** aponta para o servidor de autenticação que você deseja (nuvem ou local)
- Rode o projeto

### Autenticação:

O projeto utiliza autenticação do projeto [Jurify.Autenticador](https://github.com/gustavoosantoos/Jurify.Autenticador) para realizar as operações. É utilizado o padrão Bearer JWT.

Para obter um token e realizar as operações, seguir os seguintes passos:

- Baixar o Postman
- Fazer uma requisição com as seguintes configurações: 
[![5cUU0.png](https://b.imge.to/2019/07/19/5cUU0.png)](https://imge.to/i/5cUU0)

|Campo|Valor|
|-----|-----|
|Método|POST|
|URL|http://jurify-autenticador.azurewebsites.net/connect/token|
|Tipo do corpo|x-www-form-urlencoded|
|scope|jurify.api.lawyers|
|grant_type|password|
|username|gustavo|
|password|gustavo|
|client_id|jurify.web.lawyers|
|client_secret|jurify.web.lawyers|

- O endpoint retornada um object com o access_token incluso
- Copiar somente o token e no Swagger da aplicação, clicar em Authorize e adicionar o valor: "Bearer SEU_TOKEN" (ignore as aspas) 
