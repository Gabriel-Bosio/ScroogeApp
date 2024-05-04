using System;
using System.Collections.Generic;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO;

namespace ScroogeBackend.Dominio
{
    public class Gasto
    {
        private GastoDAO conexao;
        private CategoriaGastoDAO ligacaoCategoria;

        public Gasto()
        {
            conexao = new GastoDAO();
            ligacaoCategoria = new CategoriaGastoDAO();
        }

        public int inserirGasto(GastoDTO novoGasto)
        {
            int idRetorno;
            CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
            try
            {
                categoriaGasto = ligacaoCategoria.obterPorId(novoGasto.id_categoriaGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (categoriaGasto == null) throw new Exception();

            novoGasto.dataHoraGasto = DateTime.Now;

            try
            {
                idRetorno = conexao.inserir(novoGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idRetorno;
        }

        public double somarGastos(int id_categoria, DateTime periodo)
        {
            double somaGastos = 0;
            DateTime inicio = new DateTime(periodo.Year, periodo.Month, 1, 0, 0, 0);
            DateTime fim = inicio.AddMonths(1).AddSeconds(-1);
            try
            {
                somaGastos = conexao.obterSoma(inicio, fim, id_categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return somaGastos;
        }

        public double somarGastos()
        {
            double somaGastos = 0;
            try
            {
                somaGastos = conexao.obterSoma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return somaGastos;
        }

        public List<GastoDTO> listarGastos()
        {
            List<GastoDTO> gastos;
            try
            {
                gastos = conexao.obterTodos();
                foreach (var gasto in gastos)
                {
                    gasto.categoria = ligacaoCategoria.obterPorId(gasto.id_categoriaGasto);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gastos;
        }

        public GastoDTO obterGasto(int id)
        {
            GastoDTO gasto;
            try
            {
                gasto = conexao.obterPorId(id);
                gasto.categoria = ligacaoCategoria.obterPorId(gasto.id_categoriaGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gasto;
        }

        public void removerGasto(int id)
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
