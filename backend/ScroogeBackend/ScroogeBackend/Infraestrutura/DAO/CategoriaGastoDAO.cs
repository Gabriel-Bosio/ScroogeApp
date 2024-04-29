using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using ScroogeBackend.Infraestrutura.DTO;

namespace ScroogeBackend.Infraestrutura.DAO;
public class CategoriaGastoDAO
{
    private readonly string _connectionString = @"Data Source=C:\Workspace\EngDeSoftware2\Scrooge.db";


    public int inserir(string descricao, double? limiteCategoria, bool removivel = true)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO CategoriaGasto (descricao, limiteCategoria, removivel)
                    VALUES (@descricao, @limiteCategoria, @removivel)";
            command.Parameters.AddWithValue("@descricao", descricao);
            command.Parameters.AddWithValue("@limiteCategoria", limiteCategoria ?? 0);
            command.Parameters.AddWithValue("@removivel", removivel ? 1 : 0);
            command.ExecuteNonQuery();

            command.CommandText = "SELECT last_insert_rowid()";
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public CategoriaGastoDTO obterPorId(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM CategoriaGasto WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new CategoriaGastoDTO
                    {
                        id = reader.GetInt32(0),
                        descricao = reader.GetString(1),
                        limiteCategoria = reader.IsDBNull(2) ? null : (double?)reader.GetDouble(2),
                        removivel = reader.GetInt32(3) == 1
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public void atualizarPorId(int id, double limite)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE CategoriaGasto SET limiteCategoria = @limite WHERE id = @id";
            command.Parameters.AddWithValue("@limite", limite);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }

    public void deletarPorId(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    DELETE FROM CategoriaGasto WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }

    public List<CategoriaGastoDTO> obterTodos()
    {
        List<CategoriaGastoDTO> categorias = new List<CategoriaGastoDTO>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM CategoriaGasto";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categorias.Add(new CategoriaGastoDTO
                    {
                        id = reader.GetInt32(0),
                        descricao = reader.GetString(1),
                        limiteCategoria = reader.IsDBNull(2) ? null : (double?)reader.GetDouble(2),
                        removivel = reader.GetInt32(3) == 1
                    });
                }
            }
        }

        return categorias;
    }
}
