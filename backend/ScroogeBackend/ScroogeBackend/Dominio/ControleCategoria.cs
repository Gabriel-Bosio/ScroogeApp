using System;
using System.Collections.Generic;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;
using ScroogeBackend.Infraestrutura.DTO.ControleCategoria;

namespace ScroogeBackend.Dominio
{
    public class ControleCategoria
    {
        private ControleCategoriaDAO conexao;
        private CategoriaGasto ligacaoCategoria;
        private Gasto ligacaoGasto;

        public ControleCategoria()
        {
            conexao = new ControleCategoriaDAO();
            ligacaoCategoria = new CategoriaGasto();
            ligacaoGasto = new Gasto();
        }

        public int inserirControle(int id_categoria, DateTime mesBase)
        {
            int idRetorno;
            CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
            try
            {
                categoriaGasto = ligacaoCategoria.obterCategoria(id_categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (categoriaGasto == null) throw new Exception();

            try
            {
                idRetorno = conexao.inserir(new ControleCategoriaDTO
                {
                    limite = categoriaGasto.limiteCategoria,
                    gastoAtual = 0,
                    mesBase = mesBase,
                    mensagem = "Nenhuma",
                    id_categoriaGasto = categoriaGasto.id
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idRetorno;
        }

        public void alterarControle(int id_categoriaGasto, DateTime mesBase)
        {
            ControleCategoriaDTO controleAtualizado = new ControleCategoriaDTO();
            try
            {
                controleAtualizado = conexao.obterControle(id_categoriaGasto, mesBase);
                if (controleAtualizado == null)
                {
                    if(mesBase.Month == DateTime.Now.Month && mesBase.Year == DateTime.Now.Year)
                    {
                        inserirControle(id_categoriaGasto, mesBase);
                        controleAtualizado = conexao.obterControle(id_categoriaGasto, mesBase);
                        alterarControle(id_categoriaGasto, mesBase.AddMonths(-1));
                    }
                }
                if(controleAtualizado != null)
                {
                    controleAtualizado.categoria = ligacaoCategoria.obterCategoria(controleAtualizado.id_categoriaGasto);
                    controleAtualizado = calcularGasto(controleAtualizado);
                    conexao.atualizarPorId(controleAtualizado.id, controleAtualizado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControleCategoriaDTO> listarControles(DateTime mesBase)
        {
            List<ControleCategoriaDTO> controles;
            try
            {
                List<CategoriaGastoDTO> categorias = ligacaoCategoria.listarCategorias();
                categorias = ligacaoCategoria.listarCategorias();
                foreach (var categoria in categorias)
                {
                    alterarControle(categoria.id, mesBase);
                }
                controles = conexao.obterTodos(mesBase);
                foreach(var controle in controles)
                {
                    controle.categoria = ligacaoCategoria.obterCategoria(controle.id_categoriaGasto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return controles;
        }
        private ControleCategoriaDTO calcularGasto(ControleCategoriaDTO controle)
        {
            double gastoTotal = 0;

            try
            {
                gastoTotal = ligacaoGasto.somarGastos(controle.id_categoriaGasto, controle.mesBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (controle.limite == 0)
            {
                controle.mensagem = $"Não há limite para {controle.categoria.descricao}";
            }
            else if(gastoTotal == 0)
            {
                controle.mensagem = $"Você ainda não gastou em {controle.categoria.descricao} neste mês";
            }
            else if(gastoTotal > controle.limite)
            {
                controle.mensagem = $"Você estourou o limite para {controle.categoria.descricao} neste mês";
            }
            else controle.mensagem = $"Restam R${controle.limite - gastoTotal} para estourar o limite deste mês";

            controle.gastoAtual = gastoTotal;

            return controle;
        }
    }
}
