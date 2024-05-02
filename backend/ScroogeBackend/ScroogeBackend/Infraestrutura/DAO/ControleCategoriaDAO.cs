using ScroogeBackend.Infraestrutura.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public class ControleCategoriaDAO : BaseDAO
    {
        public int inserir(ControleCategoriaDTO novoControle)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO ControleCategoria (limite, gastoAtual, mesBase, mensagem, id_categoriaGasto)
                    VALUES (@limite, @gastoAtual, @mesBase, @mensagem, @id_categoriaGasto)";
                    command.Parameters.AddWithValue("@limite", novoControle.limite);
                    command.Parameters.AddWithValue("@gastoAtual", novoControle.gastoAtual);
                    command.Parameters.AddWithValue("@mesBase", novoControle.mesBase);
                    command.Parameters.AddWithValue("@mensagem", novoControle.mensagem);
                    command.Parameters.AddWithValue("@id_categoriaGasto", novoControle.id_categoriaGasto);
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

        public void atualizarPorId(int id, ControleCategoriaDTO controleAlterado)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE ControleCategoria SET " +
                                          "limiteCategoria = @limite, gastoAtual = @gastoAtual, mensagem = @mensagem " +
                                          "WHERE id = @id";
                    command.Parameters.AddWithValue("@limite", controleAlterado.limite);
                    command.Parameters.AddWithValue("@gastoAtual", controleAlterado.gastoAtual);
                    command.Parameters.AddWithValue("@mensagem", controleAlterado.mensagem);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControleCategoriaDTO obterControle(int id_controle, DateTime mesBase)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM ControleGasto WHERE id_categoriaGasto = @id_categoriaGasto and mesBase = @mesBase";
                    command.Parameters.AddWithValue("@id_categoriaGasto", id_controle);
                    command.Parameters.AddWithValue("@mesBase", mesBase);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ControleCategoriaDTO
                            {
                                id = reader.GetInt32(0),
                                limite = reader.GetDouble(2),
                                gastoAtual = reader.GetDouble(3),
                                mesBase = reader.GetDateTime(4),
                                mensagem = reader.GetString(5),
                                id_categoriaGasto = reader.GetInt32(6)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControleCategoriaDTO> obterTodos(DateTime mesBase)
        {
            List<ControleCategoriaDTO> controles = new List<ControleCategoriaDTO>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT id, id_usuario, limite, gastoAtual, mesBase, mensagem, id_categoriaGasto FROM Gasto " +
                                          "INNER JOIN CategoriaGasto as cg ON cg.id = id_categoriaGasto " +
                                          "WHERE dataHoraGasto = @mesBase ORDER BY cg.removivel DESC, cg.descricao ASC";

                    command.Parameters.AddWithValue("@mesBase", mesBase);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            controles.Add(new ControleCategoriaDTO
                            {
                                id = reader.GetInt32(0),
                                limite = reader.GetDouble(2),
                                mesBase = reader.GetDateTime(3),
                                mensagem = reader.GetString(4),
                                id_categoriaGasto = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return controles;
        }
    }
}
