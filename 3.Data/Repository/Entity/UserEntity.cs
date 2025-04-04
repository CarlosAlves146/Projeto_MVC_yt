using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entity
{
    public class UserEntity
    {
        public int Id { get; set;}
        public string? NomeCompleto { get; set;}
        public string? Type { get; set;}
        public string? Resethash { get; set;}
        public string? Usuario { get; set;}
        public string? Email { get; set;}
        public DateTime? Nascimento { get; set; }
        public string? Senha { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public PersonEntity? Person { get; set; }
    }

}