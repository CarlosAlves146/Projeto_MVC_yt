using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using FluentValidation;
using FluentValidation.Results;

using Projeto.Controllers;
public class LoginController : Controller
{
    // Define um método chamado Index, que será acionado quando o usuário acessar 
    // Login no navegador.

    // IActionResult → Indica que esse método retorna uma ação para a resposta HTTP. Ele pode retornar:
    // Uma View (return View();) - Um JSON (return Json(obj);). - Um redirecionamento (return Redirect("URL");).
    public IActionResult Index()
    { 
        // criando um objeto UserViewModel.
        // Lembrando que se na tela de login temos dois campos atrelados
        // Ao Objeto UserViewModel, temos que criar essa instancia aqui.
        CadastroViewModel user = new CadastroViewModel();
        return View("Index", user);
    }

    [HttpPost]
    public IActionResult Aut(CadastroViewModel user)
    {
        // Objeto validator da tela de cadastro/login apenas.
        // Pq o Objeto que estamos criando aqui contem todas validações da class
        // do usuario existentes, porem aqui no login vamos validar somente dois campos
        // caso o usuário já tenha conta.
        LoginValidator validator = new LoginValidator();
        ValidationResult results = validator.Validate(user);

        // Se a validação falhar
        if(!results.IsValid)
        {
            // Verifica as falhas de validação e as coloca em ViewData para serem exibidas na tela
            foreach(var falha in results.Errors)
            {
                if(falha.PropertyName == "Login")
                {   
                    Console.WriteLine("Propriedade ->" + falha.PropertyName + "<- falha na validação, o erro foi: ->" + falha.ErrorMessage);
                    Console.WriteLine("Flag login");
                    continue;
                }else if(falha.PropertyName == "Senha")
                {
                    Console.WriteLine(user.Senha);
                    Console.WriteLine("Propriedade ->" + falha.PropertyName + "<- falha na validação, o erro foi: ->" + falha.ErrorMessage);
                    Console.WriteLine("Flag Senha");
                    continue;
                }
                Console.WriteLine("Propriedade ->" + falha.PropertyName + "<- falha na validação, o erro foi: ->" + falha.ErrorMessage);
            }
            Console.WriteLine("-----------------------------------");
        }
        return View("Index", user);
    }

    
    public IActionResult Forgot()
    { 
        return View();
    }

    public IActionResult Reset()
    { 
        return View();
    }
}