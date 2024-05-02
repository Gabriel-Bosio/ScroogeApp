using System;
using System.Collections.Generic;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO;

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
                conexao.deletarPorId(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

