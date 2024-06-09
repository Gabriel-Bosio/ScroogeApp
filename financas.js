// Função para obter todas as categorias de gasto e exibir como cards
function getAllCategoriaGastosCards() {
    fetch('https://localhost:7248/ControleCategoria/Todos?mesBase=2024-06-01T00%3A00%3A00.000Z', {
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
            // Limpa a área dos cards
            const cardsContainer = document.getElementById('cards-container');
            cardsContainer.innerHTML = '';

            // Limpa a área dos modais de edição
            const modalEditarContainer = document.getElementById('modalEditar-container');
            modalEditarContainer.innerHTML = '';

            // Limpa a área dos modais de exclusão
            const modalExcluirContainer = document.getElementById('modalExcluir-container');
            modalEditarContainer.innerHTML = '';

            // Cria os modais de excluir com os dados recebidos
            data.forEach(c => {
                const modalExcluir = document.createElement('span');

                modalExcluir.innerHTML = `
                    <!-- Modal for "Excluir Categoria ${c.categoria.descricao}"-->
    <div class="modal fade" id="ExcluirCategoria${c.id_categoriaGasto}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabel">Excluir categoria</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h5 class="modal-title" id="modalErroLabel">Você tem certeza que deseja excluir a categoria ${c.categoria.descricao}?</h5>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn" data-bs-dismiss="modal"
                        style="background-color: #2c2c2c; color: #98DD78; border: 2px solid #2c2c2c;">Fechar</button>
                    <button type="button" class="btn" onclick="deleteCategoria(${c.id_categoriaGasto})"
                        style="background-color: #98DD78; color: #2c2c2c; border: 2px solid #2c2c2c;">Excluir</button>
                </div>
            </div>
        </div>
    </div>
                `;
                
                modalExcluirContainer.appendChild(modalExcluir);
            })

            // Cria os modais de editar com os dados recebidos
            data.forEach(c => {
                const modalEditar = document.createElement('span');

                modalEditar.innerHTML = `
                <!-- Modal for "Editar Categoria ${c.categoria.descricao}"-->
        <div class="modal fade" id="EditarCategoria${c.id_categoriaGasto}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
            aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabel">Editar Categoria</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <label for="valor" class="form-label">Digite um novo nome: <span style="color: red;">* (obrigatório)</span></label>
                        <input id="inputDescricaoEditarCategoria${c.id_categoriaGasto}" class="form-control" type="text" value="${c.categoria.descricao}"
                            aria-label="${c.categoria.descricao}" required>
                        <label for="valor" class="form-label">Digite um novo limite: <span style="color: red;">* (obrigatório)</span></label>
                        <input id="inputLimiteEditarCategoria${c.id_categoriaGasto}" class="form-control" type="text" value="${c.categoria.limiteCategoria}" aria-label="${c.categoria.limiteCategoria}" required>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn" data-bs-dismiss="modal"
                            style="background-color: #2c2c2c; color: #98DD78; border: 2px solid #2c2c2c;">Fechar</button>
                        <button type="button" class="btn" onclick="alterCategoria(${c.id_categoriaGasto})"
                            style="background-color: #98DD78; color: #2c2c2c; border: 2px solid #2c2c2c;">Salvar</button>
                    </div>
                </div>
            </div>
        </div>
                `;
                
                modalEditarContainer.appendChild(modalEditar);
            })

            // Cria os cards com os dados recebidos
            data.forEach(c => {
                const porcentagem = (c.gastoAtual / c.categoria.limiteCategoria) * 100
                const card = document.createElement('div');
                card.className = 'card';
                card.style = 'margin-top: 10px';

                card.innerHTML = `
                <div class="card-body">
                    <h4 class="card-title">${c.categoria.descricao}</h4>
                    <p class="card-text">
                        Valor do limite: ${c.categoria.descricao === "Outros" ? "Não possui limite" : `R$${c.categoria.limiteCategoria.toFixed(2)}`}<br>
                        Valor de gasto: R$${c.gastoAtual.toFixed(2)} <br>
                        
                    </p>
                    <p class="card-text text-center" style="margin: 0 0 0 0;">
                        ${c.categoria.descricao !== "Outros" ? `${porcentagem.toFixed(2)}%` : ''}
                    </p>
                    ${c.categoria.descricao !== "Outros" ? `
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar"
                            aria-valuenow="${porcentagem}" aria-valuemin="0" aria-valuemax="100" style="width: ${porcentagem}%">
                        </div>
                    </div>` : ''}

                    ${c.categoria.descricao !== "Outros" ? `
                        <div class="d-flex justify-content-between">
                        <div class="ms-left p-md-1 my-1">
                            <button type="button" class="btn" data-bs-toggle="modal" style="margin-top: 12px;" data-bs-target="#EditarCategoria${c.id_categoriaGasto}" data-id="${c.id_categoriaGasto}">
                                <svg xmlns="http://www.w3.org/2000/svg" style="color:#2c2c2c;" width="28" height="28" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                    <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                                </svg>
                                <p class="card-text">Editar</p>
                            </button>
                        </div>
                        <div class="ms-right p-md-1 my-1">
                            <button type="button" style="margin-top: 12px;" class="btn" data-bs-toggle="modal" data-bs-target="#ExcluirCategoria${c.id_categoriaGasto}">
                                <svg xmlns="http://www.w3.org/2000/svg" style="color:#2c2c2c;" width="28" height="28" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                                    <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5M8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5m3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0"/>
                                </svg>
                                <p class="card-text">Excluir</p>
                            </button>
                        </div>
                    </div>
                        ` : ''}
                </div>
            `;
                cardsContainer.appendChild(card);
            });
        })
        .catch(error => {
            console.error('Erro ao buscar as categorias criadas!', error);
        });
}

function getAllCategoriaGastosSelect() {
    fetch('https://localhost:7248/ControleCategoria/Todos?mesBase=2024-06-01T00%3A00%3A00.000Z')
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao obter categorias do select');
            }
            return response.json();
        })
        .then(categorias => {
            const selectElement = document.getElementById('selectCategoriaGasto');

            // Adicionar opção padrão
            const defaultOption = document.createElement('option');
            defaultOption.text = 'Selecione uma opção';
            defaultOption.value = 'none';
            selectElement.appendChild(defaultOption);

            // Adicionar as categorias
            categorias.forEach(c => {
                const optionElement = document.createElement('option');
                optionElement.value = c.categoria.id;
                optionElement.text = c.categoria.descricao;
                selectElement.appendChild(optionElement);
            });
        })
        .catch(error => {
            console.error('Erro:', error);
        });
}

function getSaldo() {
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

async function deleteCategoria(id_categoria) {
      try {
        const response = await fetch(`https://localhost:7248/CategoriaGasto/${id_categoria}`, {
            method: 'DELETE',
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} ${response.statusText}`);
        }

        $('#modalSucessoDeletarCategoria').modal('show');
    } catch (error) {
        $('#modalErroDeletarCategoria').modal('show');
    }
}

function alterCategoria(id_categoria) {
    // Pega os valores dos campos de entrada
    const descricao = document.getElementById('inputDescricaoEditarCategoria'+id_categoria).value;
    const limiteCategoria = document.getElementById('inputLimiteEditarCategoria'+id_categoria).value;

    // Verifica se os dois campos estão vazio
    if ((limiteCategoria === '') && (descricao === '')) {
        $('#modalErroCamposObrigatoriosVazios').modal('show');
        return; // Retorna para interromper o envio
    }else if(descricao === ''){ // Verifica se o campo nome esta vazio
        $('#modalErroCampoNomeVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria === ''){ // Verifica se o campo limite esta vazio
        $('#modalErroCampoLimiteVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if(isNaN(parseFloat(limiteCategoria))){ // Verifica se o campo limite é valido
        $('#modalErroLimiteNaoPodeSerTexto').modal('show');
        return; // Retorna para interromper o envio
    }else if(parseFloat(descricao)){ // Verifica se o campo nome nao pode ser numero
        $('#modalErroNomeNaoPodeSerNumero').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria < 0){ // Verifica se o campo limite nao pode ser negativo
        $('#modalErroLimiteNaoPodeSerNegativo').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria == 0){ // Verifica se o campo limite nao pode ser negativo
        $('#modalErroLimiteNaoPodeSerZero').modal('show');
        return; // Retorna para interromper o envio
    }

    // Cria um objeto JSON com o valor
    const jsonData = {
        id: id_categoria,
        descricao: descricaoCategoria,
        limiteCategoria: parseFloat(limite) // Convertendo para double
    };

    // Envia o JSON para o backend
    fetch('https://localhost:7248/CategoriaGasto', {
        method: 'PUT', // Alterado para PUT para alterar dados
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
            console.error('Alterado com sucesso!', data);
        })
        .catch(error => {
            console.error('Erro ao alterar:', error);
        });
}

function setRenda() {
    // Pega o valor do campo de entrada
    const valor = document.getElementById('input-valor').value;

    // Verifica se o campo de entrada está vazio
    if (valor === '') {
        $('#modalErroCampoValorRendaNaoPodeSerVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if (isNaN(parseFloat(valor))) { // Verifica se o campo de entrada é do tipo double
        $('#modalErroCampoValorRendaNaoPodeSerTexto').modal('show');
        return; // Retorna para interromper o envio
    }else if (valor < 0) { // Verifica se o campo de entrada não é negativo
        $('#modalErroCampoValorRendaNaoPodeSerNegativo').modal('show');
        return; // Retorna para interromper o envio
    }else if (valor == 0) { // Verifica se o campo de entrada não é igual a zero
        $('#modalErroCampoValorRendaNaoPodeSerZero').modal('show');
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
    const limiteCategoria = document.getElementById('inputLimite').value;
    const removivel = true; // ou false, dependendo do seu requisito

    // Verifica se os dois campos estão vazio
    if ((limiteCategoria === '') && (descricao === '')) {
        $('#modalErroCamposObrigatoriosVazios').modal('show');
        return; // Retorna para interromper o envio
    }else if(descricao === ''){ // Verifica se o campo nome esta vazio
        $('#modalErroCampoNomeVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria === ''){ // Verifica se o campo limite esta vazio
        $('#modalErroCampoLimiteVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if(isNaN(parseFloat(limiteCategoria))){ // Verifica se o campo limite é valido
        $('#modalErroLimiteNaoPodeSerTexto').modal('show');
        return; // Retorna para interromper o envio
    }else if(parseFloat(descricao)){ // Verifica se o campo nome nao pode ser numero
        $('#modalErroNomeNaoPodeSerNumero').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria < 0){ // Verifica se o campo limite nao pode ser negativo
        $('#modalErroLimiteNaoPodeSerNegativo').modal('show');
        return; // Retorna para interromper o envio
    }else if(limiteCategoria == 0){ // Verifica se o campo limite nao pode ser negativo
        $('#modalErroLimiteNaoPodeSerZero').modal('show');
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
    const selectCategoriaId = document.getElementById("selectCategoriaGasto");
    const categoriaId = selectCategoriaId.options[selectCategoriaId.selectedIndex].value;

    // Verifica se os dois campos estão vazio
    if ((valor === '') && (categoriaId === 'none')) {
        $('#modalErroCamposObrigatoriosVazios').modal('show');
        return; // Retorna para interromper o envio
    }else if(valor === ''){ // Verifica se o campo valor esta vazio
        $('#modalErroCampoValorGastoVazio').modal('show');
        return; // Retorna para interromper o envio
    }else if(categoriaId === 'none'){ // Verifica se o campo de selecionar categoria esta vazio
        $('#modalErroNenhumaCategoriaSelecionada').modal('show');
        return; // Retorna para interromper o envio
    }else if (isNaN(parseFloat(valor))) { // Verifica se o campo de valor é do tipo double
        $('#modalErroValorGastoNaoPodeSerTexto').modal('show');
        return; // Retorna para interromper o envio
    }else if (valor < 0) { // Verifica se o campo de valor é negativo
        $('#modalErroValorGastoNaoPodeSerNegativo').modal('show');
        return; // Retorna para interromper o envio
    }else if (valor == 0) { // Verifica se o campo de valor é negativo
        $('#modalErroValorGastoNaoPodeSerZero').modal('show');
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
}

function main() {
    getAllCategoriaGastosCards();

    getSaldo();

    getAllCategoriaGastosSelect();

}
main()