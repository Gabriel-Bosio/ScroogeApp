using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using ScroogeBackend.Infraestrutura.DTO;

namespace ScroogeBackend.Infraestrutura.DAO;
public class CategoriaGastoDAO : BaseDAO<CategoriaGastoDTO>
{
    public override int inserir(CategoriaGastoDTO novaCategoria)
    {
        try
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO CategoriaGasto (descricao, limiteCategoria, removivel)
                    VALUES (@descricao, @limiteCategoria, @removivel);";
                command.Parameters.AddWithValue("@descricao", novaCategoria.descricao);
                command.Parameters.AddWithValue("@limiteCategoria", novaCategoria.limiteCategoria ?? 0);
                command.Parameters.AddWithValue("@removivel", novaCategoria.removivel ? 1 : 0);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        catch (Exception ex) 
        {
            throw ex;
        }
    }

    public override void atualizarPorId(int id, CategoriaGastoDTO categoriaAlterada)
    {
        try
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE CategoriaGasto SET limiteCategoria = @limite, descricao = @descricao WHERE id = @id";
                command.Parameters.AddWithValue("@limite", categoriaAlterada.limiteCategoria);
                command.Parameters.AddWithValue("@descricao", categoriaAlterada.descricao);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public override CategoriaGastoDTO obterPorId(int id)
    {
        try
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
                            descricao = reader.GetString(2),
                            limiteCategoria = reader.IsDBNull(3) ? null : (double?)reader.GetDouble(3),
                            removivel = reader.GetBoolean(4)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        } catch (Exception ex) 
        {
            throw ex;
        }
    }

    public override List<CategoriaGastoDTO> obterTodos()
    {
        List<CategoriaGastoDTO> categorias = new List<CategoriaGastoDTO>();

        try
        {
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
                            descricao = reader.GetString(2),
                            limiteCategoria = reader.IsDBNull(3) ? null : (double?)reader.GetDouble(3),
                            removivel = reader.GetBoolean(4)
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return categorias;
    }


    public override void deletarPorId(int id)
    {
        try
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
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    
}
