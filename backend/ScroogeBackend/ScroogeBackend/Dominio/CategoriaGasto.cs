using System;
using System.Collections.Generic;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;
using ScroogeBackend.Dominio;

namespace ScroogeBackend.Dominio
{
    public class CategoriaGasto
    {
        private CategoriaGastoDAO conexao;
        public CategoriaGasto() 
        {
            conexao = new CategoriaGastoDAO();
        }
        public int inserirCategoria(CategoriaGastoDTO novaCategoria)
        {
            int idRetorno;
            try
            {
                idRetorno = conexao.inserir(novaCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idRetorno;
        }

        public List<CategoriaGastoDTO> listarCategorias()
        {
            List<CategoriaGastoDTO> categorias;
            try
            {
                validarOutros();
                categorias = conexao.obterTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categorias;
        }

        public CategoriaGastoDTO obterCategoria(int id)
        {
            CategoriaGastoDTO categoria;
            try
            {
                categoria = conexao.obterPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categoria;
        }
        public void alterarCategoria(int id, CategoriaGastoDTO categoriaAtualizada)
        {
            try
            {
                conexao.atualizarPorId(id, categoriaAtualizada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void removerCategoria(int id)
        {
            try
            {
                Gasto ligacaoGasto = new Gasto();
                ControleCategoria ligacaoControle = new ControleCategoria();
                int idOutros = validarOutros();
                ligacaoGasto.alterarGasto(id, idOutros);
                ligacaoControle.deletarControles(id);
                conexao.deletarPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int validarOutros()
        {
            try
            {
                int idOutros = conexao.obterOutros();
                if (idOutros == 0)
                {
                    idOutros = conexao.inserir(new CategoriaGastoDTO
                    {
                        descricao = "Outros",
                        limiteCategoria = 0,
                        removivel = false
                    });
                }
                return idOutros;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

