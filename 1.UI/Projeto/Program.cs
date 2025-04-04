// Criando um construtor para a aplicação, 
// permitindo adicionar serviços e configurações.
var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços.
// Registra o MVC no projeto, permitindo que 
// sua aplicação utilize Controllers e Views.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor HSTS padrão é 30 dias. Você pode querer mudar
    // isso para cenários de produção, veja https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//     Middleware:
// 💡 No ASP.NET Core, cada um desses passos é um Middleware!
//     Eles processam a requisição e decidem se ela pode continuar ou se será bloqueada.
app.UseHttpsRedirection();   // 1️⃣ Redireciona HTTP para HTTPS

app.UseRouting();            // 2️⃣ Define o sistema de rotas

app.UseAuthorization();      // 3️⃣ Controla permissões de acesso, Agora podemos bloquear ou permitir acesso

app.MapStaticAssets();       // 4️⃣ Configura arquivos estáticos, esse Middleware carrega todos arquivos js e css.

app.MapControllerRoute(      // 5️⃣ Define a estrutura de URLs
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
