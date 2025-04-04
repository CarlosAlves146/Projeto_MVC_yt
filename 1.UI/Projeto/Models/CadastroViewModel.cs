using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Projeto.Controllers;

namespace Projeto.Models;
//MODEL


public class CadastroViewModel
{
    public string? NomeCompleto { get; set; }
    public string? Usuario { get; set; }
    public string? Email { get; set; }
    public string? Login { get; set; }
    public DateTime? Nascimento { get; set; }
    public string? Senha { get; set; }
    public string? ConfirmacaoSenha { get; set; }
    public string? MensagemSucesso { get; set; }
}



