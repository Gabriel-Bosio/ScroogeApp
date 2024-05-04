using ScroogeBackend.Infraestrutura.DTO.Renda;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public class RendaDAO : BaseDAO
    {
        public int inserir(RendaDTO novaRenda)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO Renda (valorRenda, dataHoraRenda)
                    VALUES (@valorRenda, @dataHoraRenda)";
                    command.Parameters.AddWithValue("@valorRenda", novaRenda.valorRenda);
                    command.Parameters.AddWithValue("@dataHoraRenda", novaRenda.dataHoraRenda);
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
                    DELETE FROM Renda WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public RendaDTO obterPorId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Renda WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RendaDTO
                            {
                                id = reader.GetInt32(0),
                                valorRenda = reader.GetDouble(2),
                                dataHoraRenda = reader.GetDateTime(3)
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

        public List<RendaDTO> obterTodos()
        {
            List<RendaDTO> rendas = new List<RendaDTO>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Renda";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rendas.Add(new RendaDTO
                            {
                                id = reader.GetInt32(0),
                                valorRenda = reader.GetDouble(2),
                                dataHoraRenda = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rendas;
        }

        public double obterSoma()
        {
            double somaRenda = 0;

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT sum(valorRenda) FROM Renda";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                somaRenda = Convert.ToDouble(reader.GetValue(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return somaRenda;
        }
    }
}
