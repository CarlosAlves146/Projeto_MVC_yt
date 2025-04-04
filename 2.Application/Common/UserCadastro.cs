namespace API.Common
{
    public class UserCadastro
    {
        public string? NomeCompleto { get; set;}
        public string? Usuario { get; set;}
        public string? Email { get; set;}
        public DateTime? Nascimento { get; set; }
        public string? Senha { get; set; }
        public string? ConfimacaoSenha { get; set; }

    }

    public class UserReset
    {
        public string? Senha { get; set; }
        public string? ConfimacaoSenha { get; set; }
        public string? Id { get; set; }

    }
}