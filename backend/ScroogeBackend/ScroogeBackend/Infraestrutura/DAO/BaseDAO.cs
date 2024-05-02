using ScroogeBackend.Infraestrutura.DTO;
using System.Data.SQLite;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public abstract class BaseDAO
    {
        public readonly string _connectionString = @"Data Source=C:\Workspace\EngDeSoftware2\ScroogeApp\backend\ScroogeApp.db";
    }
}
