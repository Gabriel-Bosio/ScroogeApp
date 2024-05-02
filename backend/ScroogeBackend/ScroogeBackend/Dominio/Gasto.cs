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

        public List<GastoDTO> listarGastos(int id_categoria, DateTime periodo)
        {
            List<GastoDTO> gastos;
            DateTime inicio = new DateTime(periodo.Year, periodo.Month, 1, 0, 0, 0);
            DateTime fim = inicio.AddMonths(1).AddSeconds(-1);
            try
            {
                gastos = conexao.obterTodos(inicio, fim, id_categoria);
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
