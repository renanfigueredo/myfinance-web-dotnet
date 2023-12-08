using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain;

namespace myfinance_web_netcore.Infrastructure
{
    public class MyFinanceDbContext : DbContext  //@ informa que a classe herda de DbContext
    {
        public DbSet<PlanoConta> PlanoConta {get; set;} //@ é o objeto que permide acessar a entidade de dominio (a classe desejada que acesa o DB, geralmente ela é similar a aparencia no DB )

        public DbSet<Transacao> Transacao {get; set;} //@ é o objeto que permide acessar a entidade de dominio (a classe desejada que acesa o DB, geralmente ela é similar a aparencia no DB )

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //@ Sobrescreve o método para informar o que o cód deve fazer no meu projeto
        {
            var connectionString = @"Server=Renan-Not\SQLEXPRESS01;Database=myfinanceweb;Trusted_Connection=True;TrustServerCertificate=True;"; //! O nome tem que ser identico ao padrão do site ( https://www.connectionstrings.com/sql-server/ )
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
} 

//@ Permite realizar a comunicação do projeto com o banco de dados
//* Para ambiente de produção, geralmente usamos essa connectionString:  "Data Source=.;Initial Catalog=banco;User ID=sa;Password=***;TrustServerCertificate=True;"