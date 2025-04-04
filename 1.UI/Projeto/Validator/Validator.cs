using System.ComponentModel.DataAnnotations;
using System.Data;
using FluentValidation;
using Projeto.Models;
using System.Text.RegularExpressions;

namespace Projeto.Controllers
{
    // Validator Cadastro Completo
    // AbstractValidator<T> é uma classe genérica do FluentValidation que permite criar regras de validação.
    // Ligação onde irá pegar todas as propriedades da nossa model\/ <CadastroViewModel>
    public class UserValidator : AbstractValidator<CadastroViewModel>
    {
        // Construtor
        public UserValidator()
        {
            //----------------------------------------------------
            RuleFor(user => user.NomeCompleto).NotNull().WithMessage("Nome é obrigatório");
            //----------------------------------------------------
            RuleFor(user => user.Usuario).NotNull().WithMessage("Usuario Obrigatório");
            //----------------------------------------------------
            RuleFor(user => user.Email).NotNull().WithMessage("E-mail não pode ser nulo")
            .NotEmpty().WithMessage("E-mail não pode estar vazio")
            .Must(ValidaEmail).WithMessage("Formato de E-mail incorreto.");
            //----------------------------------------------------
            RuleFor(user => user.Nascimento)
            .NotEmpty()
            .WithMessage("Informe a data de nascimento")
            .LessThan(DateTime.Today).WithMessage("A data de Nascimento não pode estar no futuro")
            .InclusiveBetween(new DateTime(1900, 1, 1), DateTime.Today).WithMessage("A data de nascimento deve estar entre 01/01/1900 e hoje!");
            //----------------------------------------------------
            RuleFor(user => user.Senha).NotNull().WithMessage("Campo senha Null")
            .NotEmpty().WithMessage("Campo senha vazio")
            .MinimumLength(6).WithMessage("Senha não tem o minimo de caracteres");
           
        }
        private bool ValidaEmail(string Email)
        {
             // Verifica se é um e-mail válido
            bool isEmail = new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(Email);

            // Ira retorna true se o email estiver no formato correto e false se não estiver.
            return isEmail;
        }
    }
    

    // Validator Tela de Login.
    public class LoginValidator : AbstractValidator<CadastroViewModel>
    {
        // Construtor
        public LoginValidator()
        {
            //----------------------------------------------------
            RuleFor(user => user.Login)
            .NotNull().WithMessage("Usuario é obrigatório")
            .NotEmpty().WithMessage("Usuário ou e-mail é obrigatório!")
            .Must(ValidaNomeOuEmail).WithMessage("Informe e-mail ou usuário válido!")          
            .Must(ConexaoBd).WithMessage("Usuário não encontrado");          
            //----------------------------------------------------
            RuleFor(user => user.Senha)
            .NotEmpty()
            .NotNull().WithMessage("Preencha o campo da senha");
        }
        private bool ValidaNomeOuEmail(string login)
        {
            // Verifica se é um e-mail válido
            bool isEmail = new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(login);
            
            // Verifica se é um nome de usuário válido (somente letras e números, pelo menos 3 caracteres)
            // using System.Text.RegularExpressions;
            bool isUsername = System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9]{3,}$");

            return isEmail || isUsername;
        }

        private bool ConexaoBd(string login)
        {
            // Verifica se é um e-mail válido utilizando o Regex, lembre-se de importar a biblioteca
            bool isEmail = Regex.IsMatch(login, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

            // Verifica se é um nome de usuário válido (somente letras e números, pelo menos 3 caracteres)
            bool isUsername = System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9]{3,}$");

            if(isEmail)
            {
                Console.WriteLine("Realizar Conexão com o BD e procurar na coluna E-mail");
                Console.WriteLine(login);
                return isEmail;
            }else if(isUsername)
            {
                Console.WriteLine("Realizar Conexão com o BD e procurar na coluna Usuário");
                Console.WriteLine(login);
                return isUsername;
            }else
            {
                return false;
            }

        }
    }
}