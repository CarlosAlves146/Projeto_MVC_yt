using Microsoft.AspNetCore.Builder;             // Permite construir e configurar o aplicativo.
using Microsoft.Extensions.DependencyInjection; // Gerencia serviços (como APIs, autenticação, banco de dados).
using Microsoft.Extensions.Hosting;             //  Ajuda a controlar o ciclo de vida do aplicativo (iniciar e parar corretamente).
using Microsoft.AspNetCore.Cors;
using System.Reflection;
using NSwag;

//📌 O que essa parte faz?
// ✔ Cria a aplicação web (builder).
// ✔ Carrega as configurações do projeto (como JSON, variáveis de ambiente).
// ✔ Permite adicionar serviços (como autenticação, API, banco de dados).
var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao OpenAPI com NSwag
// Isso serve para facilitar a visualização dos endpoints no Swagger!
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Minha API"; // Define o título da API como "Minha API". Este é o nome que será exibido na interface do Swagger.
    config.Version = "v1";      // Define a versão da API como "v1". Isso é útil para versionamento de API.

    // using System.Reflection;
    // Esse trecho de código tem o objetivo de incluir a documentação gerada pelos comentários XML no Swagger, melhorando a descrição da sua API
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";  // Corrigido para Assembly
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename); // Combina o caminho com o nome do arquivo XML
    if (File.Exists(xmlFilePath))  // Verifica se o arquivo XML realmente existe
    {
        config.OperationProcessors.Add(new NSwag.Generation.Processors.Security.OperationSecurityScopeProcessor("Bearer"));
        config.DocumentProcessors.Add(new NSwag.Generation.Processors.Security.SecurityDefinitionAppender("Bearer", new NSwag.OpenApiSecurityScheme
        {
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Description = "Add the bearer token to your request",
        }));
    }
});


// Aqui estamos abilitando os CORS,
builder.Services.AddCors(options =>
    options.AddPolicy("MyPolice",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
               //.WithExposedHeaders("Authorization"); // Permite que o cliente veja este cabeçalho
    }));


// Adicionando suporte a Controllers
// 📌 O que essa parte faz?
// ✔ Diz ao ASP.NET Core que queremos usar Controllers, 
// que são responsáveis por lidar com requisições HTTP,
builder.Services.AddControllers();


// Criando o app
// 📌 O que essa parte faz?
// ✔ Constrói a aplicação com as configurações feitas anteriormente.
// ✔ Agora o app está pronto para ser configurado!
var app = builder.Build();



// Configuração do pipeline
// ✔ Se a aplicação estiver em modo de desenvolvimento, ele adiciona o Swagger.
// ✔ Isso cria uma documentação interativa para testar os endpoints da API no navegador.
// 🔹 O Swagger fica acessível em: https://localhost:5001/swagger
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();    // Disponível em /swagger/v1/swagger.json
    app.UseSwaggerUi();  // Disponível em /swagger
}


app.UseCors("MyPolice");   // Adiciona o middleware do CORS
app.UseRouting();
app.UseHttpsRedirection(); // ✔ UseHttpsRedirection() → Garante que todas as requisições usem HTTPS.
app.UseAuthorization();    // ✔ UseAuthorization() → Ativa o suporte a autorização (controle de acesso).
app.MapControllers();      // ✔ MapControllers() → Diz ao ASP.NET Core para usar os Controllers da API para lidar com requisições.


// ✔ Inicia a aplicação e faz ela ficar rodando para receber requisições.
// ✔ Sem isso, a API não funciona!
app.Run();
