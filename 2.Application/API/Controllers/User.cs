using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using API.Common; // Importa o namespace que contém a classe UserModel
using FluentValidation;
using API.Controllers;
using API.Validator;
using FluentValidation.Results;
using YamlDotNet.Core.Tokens;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.WebEncoders.Testing;




namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        
        /// <summary>
        /// Autentica o usuário
        /// </summary>
        /// <param name="user">Username e Senha do usuário</param>
        /// <returns>OK se estiver OK</returns>
        /// <returns>Ok</returns>
        
        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);

            if(!results.IsValid)
            {
                // Criar uma lista de erros específicos
                var errors = results.Errors.Select(e => new 
                {
                    field = e.PropertyName,  // Nome do campo que falhou
                    message = e.ErrorMessage  // Mensagem de erro associada ao campo
                }).ToList();

                // Retorna um BadRequest com a lista de erros
                // O BadRequest é um método de retorno da classe ControllerBase, 
                // que é a classe base para controladores em APIs no ASP.NET Core.
                // Ele retorna uma resposta HTTP 400 (Bad Request), 
                // o que indica que algo está errado com a solicitação enviada pelo cliente.
                return BadRequest(new { response = "Erro de validação.", errors });
            }
            Console.WriteLine("não teve erro!");

            if(user.Senha == "123")
            {
                return Ok(new { response = "OK" });
            }else
            {
                return Ok(new { response = "ERROR" });
            }
            
        }

        /// <summary>
        /// Cadastro de usuário
        /// </summary>
        /// <param senha="senha">Senha e Confirmação de Senha</param>
        /// <returns>OK se estiver OK</returns>
        /// <returns>Ok</returns>
        [HttpPost("cadastro")]
        public IActionResult Cadastro(UserCadastro user)
        {
            UserValidatorCad validator = new UserValidatorCad();
            ValidationResult results = validator.Validate(user);

            if(!results.IsValid)
            {
                // Criar uma lista de erros específicos
                var errors = results.Errors.Select(e => new 
                {
                    field = e.PropertyName,  // Nome do campo que falhou
                    message = e.ErrorMessage  // Mensagem de erro associada ao campo
                }).ToList();

                // Retorna um BadRequest com a lista de erros
                // O BadRequest é um método de retorno da classe ControllerBase, 
                // que é a classe base para controladores em APIs no ASP.NET Core.
                // Ele retorna uma resposta HTTP 400 (Bad Request), 
                // o que indica que algo está errado com a solicitação enviada pelo cliente.
                return BadRequest(new { response = "Erro de Criação de cadastro", errors });

            }

            if(user.Senha == user.ConfimacaoSenha)
            {
                Console.WriteLine("Flag teste1");
                return Ok(new { response = "OK" });
            }else
            {
                Console.WriteLine("Flag teste2");
                return Ok(new { response = "ERROR" });
            }  
        }

        /// <summary>
        /// Resetar senha
        /// </summary>
        /// <param name="email">Username e Senha do usuário</param>
        /// <returns>OK se estiver OK</returns>
        /// <returns>Ok</returns>
        
        [HttpPost("forgot")]
        public IActionResult Forgot(UserEmail email)
        {
            UserEmailRecuperar validator = new UserEmailRecuperar();
            ValidationResult results = validator.Validate(email);

            if(!results.IsValid)
            {
                // Criar uma lista de erros específicos
                var errors = results.Errors.Select(e => new 
                {
                    field = e.PropertyName,  // Nome do campo que falhou
                    message = e.ErrorMessage  // Mensagem de erro associada ao campo
                }).ToList();

                // Retorna um BadRequest com a lista de erros
                // O BadRequest é um método de retorno da classe ControllerBase, 
                // que é a classe base para controladores em APIs no ASP.NET Core.
                // Ele retorna uma resposta HTTP 400 (Bad Request), 
                // o que indica que algo está errado com a solicitação enviada pelo cliente.
                return BadRequest(new { response = "Erro de validação.", errors });
            }
            Console.WriteLine("não teve erro!");

            if(email.Email == "carlosalves_64@hotmail.com")
            {
                return Ok(new { response = "OK" });
            }else
            {
                return Ok(new { response = "ERROR" });
            }
            
        }

         /// <summary>
        /// Resetar senha
        /// </summary>
        /// <param name="password">Username e Senha do usuário</param>
        /// <returns>OK se estiver OK</returns>
        /// <returns>Ok</returns>
        
        [HttpPost("reset")]
        public IActionResult Reset(UserReset password)
        {
            UserResetPassword validator = new UserResetPassword();
            ValidationResult results = validator.Validate(password);

            if(!results.IsValid)
            {
                // Criar uma lista de erros específicos
                var errors = results.Errors.Select(e => new 
                {
                    field = e.PropertyName,  // Nome do campo que falhou
                    message = e.ErrorMessage  // Mensagem de erro associada ao campo
                }).ToList();

                // Retorna um BadRequest com a lista de erros
                // O BadRequest é um método de retorno da classe ControllerBase, 
                // que é a classe base para controladores em APIs no ASP.NET Core.
                // Ele retorna uma resposta HTTP 400 (Bad Request), 
                // o que indica que algo está errado com a solicitação enviada pelo cliente.
                return BadRequest(new { response = "Erro de validação.", errors });
            }
            Console.WriteLine("não teve erro!");

            if(password.Senha == password.ConfimacaoSenha)
            {
                return Ok(new { response = "OK" });
            }else
            {
                return Ok(new { response = "ERROR" });
            }
            
        }

    }
    
}
