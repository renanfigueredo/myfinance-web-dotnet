# notesmyfinance-web-netcore
MyFinance - Projeto da matéria Práticas de Implementação e Evolução de Software do Curso de Pós-Graduação em Engenharia de Software da PUC-MG

-----

## Descriação
Plataforma web que possibilita às famílias registrarem suas receitas e despesas, para que possam ter uma analise de seus gastos e consequentemente desenvolver um melhor planejamento financeiro.
A aplicação permite o usuário criar categorias personalizadas para suas transações de despesas e receitas.

### Arquitetura

A arquitetura utilizada no projeto foi MVC (Model-View-Controller) que organiza o software em três componentes principais: Model (Modelo) gerencia os dados, View (Visão) responsável por exibeir informações ao usuário e passando as interações para a Controller (Controladora) que processa essas informações e realiza a comunicação com o model e atualizando a view quando necessário.

<img src="/myfinance-web-netcore/src/Image/DiagramaArquitetura.png">

### Requisitos

- ASP.NET Core: Versão 7.0

- Microsoft SQL Server

### Configurando o ambiente local

1. Clone o repositório do projeto:

   ```
   https://github.com/renanfigueredo/notesmyfinance-web-netcore.git
   ```

2. Acesse o diretório do projeto:

   ```
   cd myfinance-web-netcore
   cd src
   ```

3. Instale os pacotes do projeto:

   ```
   dotnet add package Microsoft.EntityFrameworkCore --version 7.0.13
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.13
   dotnet add package AutoMapper --version 12.0.1
   dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection  --version 12.0.1
   ```

4. Crie o banco de dados "myfinanceweb" utilizando o script "scriptDB"

