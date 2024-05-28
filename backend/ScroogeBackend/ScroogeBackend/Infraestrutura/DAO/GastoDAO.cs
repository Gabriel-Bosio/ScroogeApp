using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;
using ScroogeBackend.Infraestrutura.DTO.Gasto;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public class GastoDAO : BaseDAO
    {
        public int inserir(GastoDTO novoGasto)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO Gasto (valorGasto, dataHoraGasto, id_categoriaGasto)
                    VALUES (@valorGasto, @dataHoraGasto, @id_categoriaGasto)";
                    command.Parameters.AddWithValue("@valorGasto", novoGasto.valorGasto);
                    command.Parameters.AddWithValue("@dataHoraGasto", novoGasto.dataHoraGasto);
                    command.Parameters.AddWithValue("@id_categoriaGasto", novoGasto.id_categoriaGasto);
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

        public void deletarPorId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    DELETE FROM Gasto WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarPorId(int id_categoriaAntiga, int id_categoriaNova)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Gasto SET id_CategoriaGasto = @id_categoriaNova WHERE id_CategoriaGasto = @id_categoriaAntiga";
                    command.Parameters.AddWithValue("@id_categoriaNova", id_categoriaNova);
                    command.Parameters.AddWithValue("@id_categoriaAntiga", id_categoriaAntiga);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GastoDTO obterPorId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Gasto WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GastoDTO
                            {
                                id = reader.GetInt32(0),
                                valorGasto = reader.GetDouble(2),
                                dataHoraGasto = reader.GetDateTime(3),
                                id_categoriaGasto = reader.GetInt32(4)
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

        public List<GastoDTO> obterTodos()
        {
            List<GastoDTO> gastos = new List<GastoDTO>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Gasto";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gastos.Add(new GastoDTO
                            {
                                id = reader.GetInt32(0),
                                valorGasto = reader.GetDouble(2),
                                dataHoraGasto = reader.GetDateTime(3),
                                id_categoriaGasto = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return gastos;
        }

        public double obterSoma(DateTime inicio, DateTime fim, int id_categoria)
        {
            double somaGastos = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT sum(valorGasto) FROM Gasto " +
                                          "WHERE id_categoriaGasto = @categoria and dataHoraGasto BETWEEN @inicio and @fim";
                    command.Parameters.AddWithValue("@categoria", id_categoria);
                    command.Parameters.AddWithValue("@inicio", inicio);
                    command.Parameters.AddWithValue("@fim", fim);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(!reader.IsDBNull(0))
                                somaGastos = Convert.ToDouble(reader.GetValue(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return somaGastos;
        }

        public double obterSoma()
        {
            double somaGastos = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT sum(valorGasto) FROM Gasto";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                somaGastos = Convert.ToDouble(reader.GetValue(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return somaGastos;
        }
    }
}
