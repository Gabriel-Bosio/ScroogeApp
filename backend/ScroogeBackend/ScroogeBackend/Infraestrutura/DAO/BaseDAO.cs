using ScroogeBackend.Infraestrutura.DTO;
using System.Data.SQLite;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public abstract class BaseDAO<T>
    {
        public readonly string _connectionString = @"Data Source=C:\Workspace\EngDeSoftware2\ScroogeApp\backend\ScroogeApp.db";
        public abstract int inserir(T entity);
        public abstract void atualizarPorId(int id, T entity);
        public abstract T obterPorId(int id);

        public abstract List<T> obterTodos();

        public abstract void deletarPorId(int id);
       
    }
}
