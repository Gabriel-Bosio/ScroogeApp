using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO.EducacaoFinanceira;
using System.Reflection.PortableExecutable;

namespace ScroogeBackend.Dominio
{
    public class EducacaoFinanceira
    {
        private EducacaoFinanceiraDAO conexao;
        public EducacaoFinanceira()
        {
            conexao = new EducacaoFinanceiraDAO();
        }

        public EducacaoFinanceiraDTO buscar()
        {
            EducacaoFinanceiraDTO educacao;
            try
            {
                educacao = conexao.obter();
                if (educacao == null)
                {
                    criar();
                    educacao = conexao.obter();
                }
                return educacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void criar()
        {
            try
            {
                conexao.inserir(new EducacaoFinanceiraDTO
                {
                    conceitosTitulo = "Conceitos Básicos de Investimentos",
                    conceitosBasicos = "Investimento é a aplicação de recursos com o objetivo de obter um retorno financeiro futuro. A relação entre risco e retorno é fundamental: investimentos mais arriscados tendem a oferecer maiores retornos potenciais, por isso devemos estar bem informados para decidir onde investir. ",
                    tiposTitulo = "Tipos de Investimentos",
                    tipoInv1 = "Renda Fixa: Inclui títulos públicos, CDBs, LCIs/LCAs e debêntures. Geralmente, são mais seguros e oferecem retornos previsíveis. ",
                    tipoInv2 = "Renda Variável: Inclui ações, fundos imobiliários, ETFs e derivativos. Estes têm maior risco e potencial de retorno. ",
                    tipoInv3 = "Outros Investimentos: Fundos de investimento, criptomoedas e commodities como ouro e prata. Cada um tem características distintas de risco, retorno e liquidez. ",
                    estrategiasTitulo = "Estratégias de Investimento",
                    estrategiaInv1 = "Longo Prazo vs. Curto Prazo: Investimentos de longo prazo são menos afetados por volatilidades do mercado e podem aproveitar o crescimento composto. Investimentos de curto prazo focam em ganhos rápidos, mas são mais arriscados.",
                    estrategiaInv2 = "Análise Fundamentalista vs. Análise Técnica: A análise fundamentalista avalia a saúde financeira de uma empresa, enquanto a análise técnica foca em padrões gráficos e dados históricos de preços. ",
                    estrategiaInv3 = "Estratégias de Crescimento vs. Valor: Investir em empresas com alto potencial de crescimento (crescimento) ou em empresas subvalorizadas pelo mercado (valor). ",
                    impostosTitulo = "Impostos e Taxas",
                    impostostaxas1 = "Tributação sobre Investimentos: O Imposto de Renda é aplicável sobre os lucros de diversos investimentos, e o IOF pode incidir sobre operações de curto prazo. ",
                    impostostaxas2 = "Taxas e Custos: Incluem taxas de administração de fundos, taxa de performance, corretagem e outras. Esses custos impactam diretamente o retorno líquido dos investimentos. ",
                    impostostaxas3 = "Impacto no Retorno: Entender como impostos e taxas reduzem o lucro é essencial para escolher os investimentos mais vantajosos. ",
                    videosTitulo = "Vídeos interessantes sobre o assunto:",
                    url1 = "https://www.youtube.com/watch?v=szX_HTfkX0s",
                    url2 = "https://www.youtube.com/watch?v=gxmpHkobMyU",
                    url3 = "https://www.youtube.com/watch?v=n4uTzYN6l6I",
                    canaisTitulo = "Site com 7 canais que trazem ótimos conteúdos sobre finanças:",
                    url4 = "https://www.consorcioszk.com.br/blog/7-canais-no-youtube-para-aprender-sobre-educacao-financeira",
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
