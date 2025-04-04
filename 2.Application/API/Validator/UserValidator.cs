using System.ComponentModel.DataAnnotations;
using System.Data;
using FluentValidation;
using System.Text.RegularExpressions;
using API.Common;

namespace API.Validator
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).NotNull().WithMessage("E-mail ou usuário vazio");
            RuleFor(user => user.Senha).NotNull().NotEmpty().WithMessage("Digite a senha");
            RuleFor(user => user.Email).EmailAddress().WithMessage("E-mail está inválido");
        }
        
    }

    public class UserValidatorCad : AbstractValidator<UserCadastro>
    {
        public UserValidatorCad()
        {
            RuleFor(user => user.NomeCompleto).NotEmpty().NotNull().WithMessage("Digite seu nome completo.");
            RuleFor(user => user.Usuario).NotEmpty().NotNull().WithMessage("Escolha um usuário");
            RuleFor(user => user.Email).EmailAddress().WithMessage("E-mail está inválido");
            RuleFor(user => user.Nascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
            .Must(date => date != default(DateTime)).WithMessage("A data de nascimento é inválida.");
            RuleFor(user => user.Senha).NotNull().NotEmpty().WithMessage("Digite a senha.");
            RuleFor(user => user.ConfimacaoSenha).NotNull().WithMessage("Confirme sua senha.");
            RuleFor(user => user.Senha).Equal(o => o.ConfimacaoSenha).WithMessage("Senhas diferentes.");
        }
        
    }

    public class UserEmailRecuperar : AbstractValidator<UserEmail>
    {
        public UserEmailRecuperar()
        {
            RuleFor(user => user.Email).NotNull().WithMessage("E-mail ou usuário vazio");
            RuleFor(user => user.Email).EmailAddress().WithMessage("E-mail está inválido");
        }
        
    }
    public class UserResetPassword : AbstractValidator<UserReset>
    {
        public UserResetPassword()
        {
            RuleFor(user => user.Senha).NotNull().WithMessage("Campo Senha vazio");
            RuleFor(user => user.ConfimacaoSenha).NotNull().WithMessage("Campo Senha vazio");
            RuleFor(user => user.ConfimacaoSenha).Equal(o => o.Senha ).WithMessage("Senhas diferentes");
        }
        
    }
}