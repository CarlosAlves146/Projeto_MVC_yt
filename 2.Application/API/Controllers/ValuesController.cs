using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
// [Route("api/[controller]")] → Define a rota base da API. 
// O [controller] será substituído pelo nome da classe (Values), então os endpoints serão acessados por api/Values.
// [Route("api/[controller]")] → Define a rota base da API. O [controller] será substituído pelo nome da classe (Values), 
// então os endpoints serão acessados por api/Values.
public class ValuesController : ControllerBase
{
    // GET: api/Values - GET → Buscar informações
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Lista de itens");
        // Retorna a mensagem "Lista de itens". Em uma aplicação real, ele retornaria uma lista de itens do banco de dados.
    }

    // GET: api/Values/5 - GET → Buscar informações
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok($"Item {id}");
    }

    // POST: api/Values - POST → Criar informações
    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return Ok($"Criado: {value}");
    }

    // PUT: api/Values/5 - PUT → Atualizar informações
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        return Ok($"Atualizado {id} para {value}");
    }

    // DELETE: api/Values/5 - DELETE → Excluir informações
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok($"Removido item {id}");
    }
}
