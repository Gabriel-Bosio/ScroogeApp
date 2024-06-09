function getConteudoInvestimento(){
    fetch('https://localhost:7248/EducacaoFinanceira', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter os conteúdos!');
        }
        return response.json();
    })
    .then(data => {
        displayEducacaoFinanceira(data);
    })
    .catch(error => {
        console.error('Erro ao buscar os conteúdos!', error);
    });

}

function displayEducacaoFinanceira(data) {
    // Aqui você pode manipular o DOM ou fazer algo mais com os dados recebidos
    document.getElementById('conceitosTitulo').innerText = data.conceitosTitulo;
    document.getElementById('conceitosBasicos').innerText = data.conceitosBasicos;
    document.getElementById('tiposTitulo').innerText = data.tiposTitulo;
    document.getElementById('tipoInv1').innerText = data.tipoInv1;
    document.getElementById('tipoInv2').innerText = data.tipoInv2;
    document.getElementById('tipoInv3').innerText = data.tipoInv3;
    document.getElementById('estrategiasTitulo').innerText = data.estrategiasTitulo;
    document.getElementById('estrategiaInv1').innerText = data.estrategiaInv1;
    document.getElementById('estrategiaInv2').innerText = data.estrategiaInv2;
    document.getElementById('estrategiaInv3').innerText = data.estrategiaInv3;
    document.getElementById('impostosTitulo').innerText = data.impostosTitulo;
    document.getElementById('impostostaxas1').innerText = data.impostostaxas1;
    document.getElementById('impostostaxas2').innerText = data.impostostaxas2;
    document.getElementById('impostostaxas3').innerText = data.impostostaxas3;
    document.getElementById('videosTitulo').innerText = data.videosTitulo;
    const videoContainer = document.getElementById('videos');
    const videosUrls = [data.url1, data.url2, data.url3];
    videosUrls.forEach((url) => {
        if (url) {
            const videoId = getYouTubeVideoId(url);
            const iframe = document.createElement('iframe');
            iframe.src = `https://www.youtube.com/embed/${videoId}`;
            iframe.allowFullscreen = true;

            const div = document.createElement('div');
            div.className = 'video-container';
            div.appendChild(iframe);

            videoContainer.appendChild(div);
        }
    });
    
    const siteLinkContainer = document.getElementById('site-link');
    const linkCard = document.createElement('div');
    linkCard.className = 'link-card';

    const linkTitle = document.createElement('h2');
    linkTitle.innerText = data.canaisTitulo;

    const linkButton = document.createElement('button');
    linkButton.style = "background-color: #98DD78; color: #2c2c2c; border: 2px solid #2c2c2c;border-radius: 10px; height: 60px;margin: 40px 0 20px 0";

    const link = document.createElement('a');
    link.href = data.url4;
    link.target = "_blank";
    link.innerText = "Clique aqui para acessar";

    siteLinkContainer.appendChild(linkCard);
    linkCard.appendChild(linkTitle);
    linkCard.appendChild(linkButton);
    linkButton.appendChild(link);
}

function getYouTubeVideoId(url) {
    const urlObj = new URL(url);
    return urlObj.searchParams.get('v');
}

document.addEventListener('DOMContentLoaded', (event) => {
    getConteudoInvestimento();
});