// O código abaixo é responsável por enviar os dados do formulário para o servidor quando o usuário clica no botão de "submit".

// O código abaixo diz que, quando o formulário for enviado (quando o botão de "submit" for pressionado),
// ele vai executar a função dentro das chaves.
$('form').on('submit', function(event){
    
    // Aqui, estamos dizendo ao navegador para não fazer o comportamento padrão de um formulário, que seria
    // recarregar a página. O comando 'event.preventDefault()' faz com que a página não seja recarregada.
    // Em vez disso, vamos enviar os dados de forma assíncrona (sem recarregar a página).
    event.preventDefault();

    // Aqui, estamos criando um objeto para armazenar os dados que o usuário preencheu no formulário.
    // Cada linha abaixo está pegando o valor de um campo do formulário (como o nome, email, senha etc.) e
    // colocando esses valores dentro do objeto 'formData'.
    var formData = {
        NomeCompleto: $("#namecompleto-146").val(), // Pega o nome completo que o usuário digitou
        Usuario: $("#usuario-146").val(),           // Pega o nome de usuário
        Email: $("#email-146").val(),               // Pega o email
        Nascimento: $("#nascimento-146").val(),     // Pega a data de nascimento
        Senha: $("#password-146").val(),            // Pega a senha
        ConfimacaoSenha: $("#confirpassword-146").val(), // Pega a confirmação da senha
    };

    // Aqui, estamos fazendo uma requisição (pedido) para o servidor. 
    // Usamos o método 'POST' para enviar os dados para o servidor, e os dados são enviados no formato JSON.

    $.ajax({
        type: "POST", // Tipo de requisição (POST é usado para enviar dados ao servidor)
        dataType: "json", // Esperamos que a resposta do servidor seja em formato JSON
        contentType: "application/json; charset=UTF-8", // Dizemos ao servidor que os dados estão sendo enviados em JSON
        data: JSON.stringify(formData), // Converte o objeto 'formData' para o formato JSON antes de enviar
        url: "http://localhost:5068/api/User/cadastro", // URL para onde os dados serão enviados
        success: function(result){ // Função que será executada caso a requisição seja bem-sucedida
            // Aqui verificamos se a resposta do servidor indica sucesso
            console.log("Resposta do servidor:", result);
            if (result.response == 'OK') {
                // Exibe uma mensagem para o usuário dizendo que o cadastro foi bem-sucedido
                alert("Cadastro Realizado com Sucesso!!");
            } else {
                // Se a resposta não for "OK", exibimos um alerta de erro
                // Exibe alert para cada erro
                
                result.response.forEach(function(error) {
                    alert("Erro no campo " + error.field + ":" + error.message)
                })
            }
        },
        error: function(xhr, status, error) { // Função para tratar erros durante a requisição
            // Caso haja algum erro, o código abaixo imprime o erro no console, para ajudar a identificar o problema.
            console.log("ERRO", error); // Exibe o erro no console
            console.log("Status", status); // Exibe o status da requisição (ex: "timeout", "erro")
            console.log("Resposta", xhr.responseText); // Exibe a resposta completa do servidor, se houver alguma
            
            try {
                // 🚀 Tenta converter a resposta do servidor de texto para um objeto JavaScript
                var response = JSON.parse(xhr.responseText);
            
                // ✅ Verifica se a resposta contém um array de erros
                if (response.errors) {
                    
                    // 🔄 Percorre cada erro e exibe um alerta para o usuário
                    response.errors.forEach(function(error) {
                        // 🛑 Mostra um alerta indicando qual campo tem erro e a mensagem correspondente
                        alert("Erro no campo " + error.field + ": " + error.message);
                    });
            
                } else {
                    // ❓ Se não houver lista de erros, exibe uma mensagem genérica
                    alert("Erro desconhecido ao cadastrar.");
                }
            
            } catch (e) {
                // ⚠️ Se ocorrer um erro ao processar a resposta (por exemplo, JSON inválido), exibe uma mensagem de erro
                alert("Erro ao processar resposta do servidor.");
            }
            
        }
    });
});
