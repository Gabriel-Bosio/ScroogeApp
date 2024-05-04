using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO.Renda;

namespace ScroogeBackend.Dominio
{
    public class Renda
    {
        private RendaDAO conexao;

        public Renda()
        {
            conexao = new RendaDAO();
        }

        public int inserirRenda(RendaDTO novaRenda)
        {
            int idRetorno = 0;

            novaRenda.dataHoraRenda = DateTime.Now;

            try
            {
                idRetorno = conexao.inserir(novaRenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idRetorno;
        }

        public double somarRenda()
        {
            double somaRenda = 0;
            try
            {
                somaRenda = conexao.obterSoma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return somaRenda;
        }

        public List<RendaDTO> listarRenda()
        {
            List<RendaDTO> renda;
            try
            {
                renda = conexao.obterTodos();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return renda;
        }

        public RendaDTO obterRenda(int id)
        {
            RendaDTO renda;
            try
            {
                renda = conexao.obterPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return renda;
        }

        public void removerRenda(int id)
        {
            try
            {
                conexao.deletarPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
