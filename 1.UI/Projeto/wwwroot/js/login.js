// O código abaixo é responsável por enviar os dados do formulário para o servidor quando o usuário clica no botão de submit.

// Esse trecho de código executa uma função quando o formulário é enviado (quando o botão "submit" é pressionado).
$('form').on('submit', function(event){
    
    // Aqui, estamos impedindo que o formulário seja enviado da forma tradicional (que recarregaria a página).
    // Com isso, podemos enviar os dados de forma assíncrona (sem recarregar a página).
    event.preventDefault();

    // Abaixo, estamos criando um objeto (que é como um "recipiente" para armazenar os dados do formulário).
    // Cada linha pega o valor inserido pelo usuário nos campos e coloca dentro desse objeto.

    var formData = {
        Email: $("#email-146").val(),
        Senha: $("#password-146").val(),            // Pega a senha do campo de input
    };

    // Agora, estamos fazendo uma requisição (um "pedido" de envio de dados) para o servidor.
    // A requisição será enviada para a URL "http://localhost:5068/api/User/cadastro".
    // Estamos usando o método "POST", que é utilizado para enviar dados para o servidor.

    $.ajax({
        type: "POST", // Tipo de requisição (POST é usado para enviar dados para o servidor)
        dataType: "json", // Esperamos que a resposta do servidor seja em formato JSON
        contentType: "application/json; charset=UTF-8", // Informamos que estamos enviando dados em formato JSON
        data: JSON.stringify(formData), // Convertendo o objeto com os dados do formulário para uma string JSON antes de enviar
        url: "http://localhost:5068/api/User/login", // URL para onde os dados serão enviados
        success: function(result){ // Função que será executada se a requisição for bem-sucedida
            // Aqui verificamos se a resposta do servidor indica sucesso
            console.log("Resposta do servidor:", result);
            if (result.response == 'OK') {
                // Se a resposta for "OK", significa que os dados foram enviados e processados com sucesso
                // Exibimos um alerta para o usuário, dizendo que tudo ocorreu bem
                alert("Login Realizado com sucesso!!");
            } else {
                // Se a resposta não for "OK", exibimos um alerta de erro
                var errors = xhr.responseJSON.errors;
                errors.forEach(function(error){
                alert("Erro no campo" + error.field + error.message)
            })
            }
        },
        error: function(xhr, status, error) { 
            // Caso ocorra algum erro durante o envio da requisição (por exemplo, se o servidor estiver fora do ar),
            // essa função será executada. Ela exibe informações detalhadas sobre o erro para ajudar a identificar o problema.
            
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
