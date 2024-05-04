using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO;
using ScroogeBackend.Infraestrutura.DTO.Saldo;

namespace ScroogeBackend.Dominio
{
    
    public class Saldo
    {
        private SaldoDAO conexao;
        private Renda ligacaoRenda;
        private Gasto ligacaoGasto;

        public Saldo()
        {
            conexao = new SaldoDAO();
            ligacaoRenda = new Renda();
            ligacaoGasto = new Gasto();
        }

        public int inserirSaldo()
        {
            try
            {
                int novoId = conexao.inserir();
                return novoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaldoDTO alterarSaldo(int id, SaldoDTO saldo)
        {
            try
            {
                saldo.valor = ligacaoRenda.somarRenda() - ligacaoGasto.somarGastos();
                conexao.atualizarPorId(id, saldo);
                return saldo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaldoDTO obterSaldo(int id)
        {
            SaldoDTO saldo;
            try
            {
                saldo = conexao.obterPorId(id);
                saldo = alterarSaldo(saldo.id, saldo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return saldo;
        }
    }
}
