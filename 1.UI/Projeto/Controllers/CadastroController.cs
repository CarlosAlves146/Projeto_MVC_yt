using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using FluentValidation.Results;

namespace Projeto.Controllers;

public class CadastroController : Controller
{
    // Define um método chamado Index, que será acionado quando o usuário acessar 
    // Login no navegador.

    // IActionResult → Indica que esse método retorna uma ação para a resposta HTTP. Ele pode retornar:
    // Uma View (return View();) - Um JSON (return Json(obj);). - Um redirecionamento (return Redirect("URL");).
    public IActionResult Index()
    { 
        // criando um objeto CadastroViewModel.
        // Lembrando que se na tela de cadastro temos 5 campos atrelados
        // Ao Objeto CadastroViewModel, temos que criar essa instância aqui.
        CadastroViewModel user = new CadastroViewModel();
        return View("Index", user);
    }

    [HttpPost]
    public IActionResult Cadast(CadastroViewModel user)
    {
        // Aqui fiz diferente criei uma propriedade na classe que recebe o nome
        // de MensagemSucesso e atribui a ela uma msg que poderia ser usado caso o 
        // pase por uma validação ou algo do tipo.
        user.MensagemSucesso = "E-mail registrado com sucesso!!";

        // Objeto validator da tela de Cadastro/login apenas.
        // Pq o Objeto que estamos criando aqui contem todas validações da class
        // do usuario existentes, porem aqui no cadastro vamos validar todos os campos do cadastro
        // completo
        UserValidator validator = new UserValidator();
        ValidationResult results = validator.Validate(user);
        Console.WriteLine("flag1");
        if(!results.IsValid)
        {
            Console.WriteLine("flag2");
            foreach(var falha in results.Errors)
            {
                Console.WriteLine("Propriedade ->" + falha.PropertyName + "<- falha na validação, o erro foi: ->" + falha.ErrorMessage);
            }
        }else
        {
            Console.WriteLine("Conexão será feita com com o BD e enviada as informações do usuario");
            Console.WriteLine(user.NomeCompleto);
            Console.WriteLine(user.Usuario);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Login);
            Console.WriteLine(user.Nascimento);
            Console.WriteLine(user.Senha);
            Console.WriteLine(user.MensagemSucesso);

            
            // Simulação: verifica se o e-mail já está cadastrado
            if (user.Email == "teste@email.com")
            {
                TempData["MensagemSucesso"] = "Este e-mail já está cadastrado!";
                return RedirectToAction("Index", "Login"); // Redireciona para tela de login
            }

            // Cadastro realizado com sucesso
            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Agora faça o login.";
            }

        return RedirectToAction("Index","Login");
    }
}