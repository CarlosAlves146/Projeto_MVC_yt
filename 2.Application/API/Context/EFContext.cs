using Repository.Entity; // Importa a entidade UserEntity, que representa a tabela no banco de dados
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação do banco de dados

namespace API.Context // Define o namespace do contexto do banco de dados
{
    public class EFContext : DbContext // Classe que representa o contexto do banco de dados, herdando de DbContext
    {
        // String de conexão com o banco de dados SQL Server Express
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=MeuBanco;Trusted_Connection=True;TrustServerCertificate=True;";

        // Aqui estou sobrescrevendo o Método que configura o contexto para usar o SQL Server, e apontar a sua string
        // de conexão.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString); // Define o provedor de banco de dados como SQL Server
        }

        // Aqui vamos configurar as relações entre as tabelas, ja que uma aponta para a outra 1:1
        // nesse método vamos utilizar o objeto ModelBuilder para dizer ao EF como mapear as classes
        // (entidades) para o banco de dados - PRINCIPALMENTE -> Relacionamentos, nomes de tabelas, tamanhos de colunas
        // chaves estrangeiras etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()            // Estamos configurando a entidade UserEntity
            .HasOne(u => u.Person)                       //...essa entidade tem um relacionamento com PersonEntity, e essa relação está representada pela propriedade Person dentro do UserEntity."
            .WithOne(p => p.User)                        //...e no outro lado da relação, a PersonEntity também tem uma propriedade chamada User que aponta de volta pro UserEntity."
            .HasForeignKey<UserEntity>(u => u.PersonId); //...e o campo PersonId da UserEntity é a chave estrangeira que liga as duas tabelas.
        }

        // Representa a tabela Users no banco de dados
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
    }
}
