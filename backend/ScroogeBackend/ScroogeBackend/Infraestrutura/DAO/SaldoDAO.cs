using System;
using System.Collections.Generic;
using System.Data.SQLite;

using ScroogeBackend.Infraestrutura.DTO.Saldo;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public class SaldoDAO : BaseDAO
    {
        public int inserir()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO Saldo (valor) VALUES (0)";
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

        public void atualizarPorId(int id, SaldoDTO saldoAlterado)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Saldo SET valor = @valor WHERE id = @id";
                    command.Parameters.AddWithValue("@valor", saldoAlterado.valor);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaldoDTO obterPorId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Saldo WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SaldoDTO
                            {
                                id = reader.GetInt32(0),
                                valor = reader.GetDouble(2)
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
    }
}