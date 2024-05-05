// Função para obter todas as categorias de gasto e exibir em uma tabela HTML
function getAllCategoriaGastosTabela() {
    fetch('https://localhost:7248/Todos?mesBase=2024-05-01T00%3A00%3A00.000Z', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter categorias de gasto');
        }
        return response.json();
    })
    .then(data => {
        // Limpa a tabela
        document.getElementById('tabela-categorias').innerHTML = '';

        // Cria as linhas da tabela com os dados recebidos
        data.forEach(c => {
            const row = document.createElement('tr');
            row.innerHTML = `
                    <td class="table-light" style="border: 2px solid #2c2c2c;">${c.categoria.descricao}</td>
                    <td class="table-light" style="border: 2px solid #2c2c2c;">${c.categoria.limiteCategoria.toFixed(2)}</td>
                    <td class="table-light" style="border: 2px solid #2c2c2c;">${c.gastoAtual.toFixed(2)}</td>
            `;
            document.getElementById('tabela-categorias').appendChild(row);
        });
    })
    .catch(error => {
        console.error('Erro:', error);
    });
}

function getAllCategoriaGastosSelect() {
    fetch('https://localhost:7248/Todos?mesBase=2024-05-01T00%3A00%3A00.000Z')
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao obter categorias do select');
            }
            return response.json();
        })
        .then(categorias => {
            const selectElement = document.getElementById('selectCategoriaGasto');
            selectElement.innerHTML = '';

            // Adicionar opção padrão
            const defaultOption = document.createElement('option');
            defaultOption.text = 'Selecione uma opção';
            selectElement.appendChild(defaultOption);

            // Adicionar as categorias
            categorias.forEach(c => {
                const optionElement = document.createElement('option');
                optionElement.value = c.categoria.id;
                optionElement.text = c.categoria.descricao;
                selectElement.appendChild(optionElement);
            });
        })
}

function getSaldo(){
    fetch('https://localhost:7248/Saldo', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter saldo');
        }
        return response.json();
    })
    .then(data => {
        // Atualiza o conteúdo da tag <p> com o valor do saldo
        document.getElementById('paragrafo-saldo').innerText = ` R$ ${data.valor.toFixed(2)}`;
    })
    .catch(error => {
        console.error('Erro:', error);
    });
}

function setRenda() {
    // Pega o valor do campo de entrada
    const valor = document.getElementById('input-valor').value;

    // Verifica se o campo de entrada está vazio
    if (valor === '') {
        $('#modalErro').modal('show');
        return; // Retorna para interromper o envio
    }
    // Verifica se o campo de entrada é do tipo double
    if (isNaN(parseFloat(valor))) {
        $('#modalErro').modal('show'); 
        return; // Retorna para interromper o envio
    }

    // Cria um objeto JSON com o valor
    const jsonData = {
        valorRenda: parseFloat(valor) // Convertendo para double
    };

    // Envia o JSON para o backend
    fetch('https://localhost:7248/Renda', {
        method: 'POST', // Alterado para POST para enviar dados
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsonData) // Converte o objeto JSON em uma string
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar valor');
        }
        return response.json();
    })
    .then(data => {
        console.error('Valor salvo!', data);
    })
    .catch(error => {
        console.error('Erro ao salvar valor:', error);
    });
}

// Função para inserir uma nova categoria de gasto
function setCategoria() {
    // Obter os valores inseridos nos campos do modal
    const descricao = document.getElementById('inputDescricao').value;
    const limiteCategoria = parseFloat(document.getElementById('inputLimite').value);
    const removivel = true; // ou false, dependendo do seu requisito

    // Verifica se o campo de entrada está vazio
    if (descricao === '' || limiteCategoria === '') {
        $('#modalErro').modal('show');
        return; // Retorna para interromper o envio
    }

    // Verifica se o campo de limite é do tipo double
    if (isNaN(parseFloat(limiteCategoria))) {
        $('#modalErro').modal('show'); 
        return; // Retorna para interromper o envio
    }

    // Criar o objeto JSON com os valores inseridos
    const novaCategoria = {
        descricao: descricao,
        limiteCategoria: limiteCategoria,
        removivel: removivel
    };

    // Enviar o objeto JSON para o backend
    fetch('https://localhost:7248/CategoriaGasto', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(novaCategoria)
    })
    
}

function setGasto() {
    const valor = document.getElementById('inputValorGasto').value;
    const categoriaId = document.getElementById('selectCategoriaGasto').value;

    // Verifica se o campo de entrada está vazio
    if (valor === '') {
        $('#modalErro').modal('show');
        return; // Retorna para interromper o envio
    }
    // Verifica se o campo de entrada é do tipo double
    if (isNaN(parseFloat(valor))) {
        $('#modalErro').modal('show'); 
        return; // Retorna para interromper o envio
    }

    fetch('https://localhost:7248/Gasto', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            valorGasto: parseFloat(valor),
            id_categoriaGasto: parseInt(categoriaId)
        })
    })

    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao enviar dados');
        }
        return response.json();
    })
    .catch(error => {
        $('#modalErro').modal('show');
        return; // Retorna para interromper o envio
    });
}

function main(){

    getAllCategoriaGastosTabela();

    getSaldo();

    getAllCategoriaGastosSelect();

}
main()