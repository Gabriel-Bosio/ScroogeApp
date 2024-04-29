using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Infraestrutura.DTO;

namespace ScroogeBackend.Dominio
{
    internal class CategoriaGasto : CategoriaGastoDTO
    {
        public CategoriaGasto(string descricao, double? limiteCategoria, bool removivel = true)
        {
            this.descricao = descricao;
            this.limiteCategoria = limiteCategoria;
            this.removivel = removivel;
        }
        public CategoriaGasto(CategoriaGastoDTO novaCategoria)
        {
            this.descricao = novaCategoria.descricao;
            this.limiteCategoria = novaCategoria.limiteCategoria;
            this.removivel = novaCategoria.removivel;
        }

        public bool alterarLimite(double limite)
        {
            this.limiteCategoria = limite;

            return true;
        }

        public int inserirCategoria()
        {
            CategoriaGastoDAO insercao = new CategoriaGastoDAO();
            int idRetorno;
            try
            {
                idRetorno = insercao.inserir(descricao, limiteCategoria, removivel);
            }catch(Exception ex)
            {
                throw new Exception();
            }
            return idRetorno;
        }

    }
}

