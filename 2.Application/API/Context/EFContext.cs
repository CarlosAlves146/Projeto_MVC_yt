using Repository.Entity; // Importa a entidade UserEntity, que representa a tabela no banco de dados
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação do banco de dados

namespace API.Context // Define o namespace do contexto do banco de dados
{
    public class EFContext : DbContext // Classe que representa o contexto do banco de dados, herdando de DbContext
    {
        // String de conexão com o banco de dados SQL Server Express
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=MeuBanco;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método que configura o contexto para usar o SQL Server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString); // Define o provedor de banco de dados como SQL Server
        }

        // Representa a tabela Users no banco de dados
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
    }
}
