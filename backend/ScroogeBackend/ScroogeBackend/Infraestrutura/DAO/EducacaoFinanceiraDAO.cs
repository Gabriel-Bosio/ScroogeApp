using ScroogeBackend.Dominio;
using ScroogeBackend.Infraestrutura.DTO.EducacaoFinanceira;
using ScroogeBackend.Infraestrutura.DTO.Gasto;
using System.Data.SQLite;

namespace ScroogeBackend.Infraestrutura.DAO
{
    public class EducacaoFinanceiraDAO : BaseDAO
    {
        public int inserir(EducacaoFinanceiraDTO educacao)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO EducacaoFinanceira (conceitosTitulo, conceitosBasicos, tiposTitulo, tipoInv1, tipoInv2, tipoInv3,
                    estrategiasTitulo, estrategiaInv1, estrategiaInv2, estrategiaInv3, impostosTitulo, impostostaxas1, impostostaxas2,
                    impostostaxas3, videosTitulo, url1, url2, url3, canaisTitulo, url4)
                    VALUES (@conceitosTitulo, @conceitosBasicos, @tiposTitulo, @tipoInv1, @tipoInv2, @tipoInv3, @estrategiasTitulo,
                    @estrategiaInv1, @estrategiaInv2, @estrategiaInv3, @impostosTitulo, @impostostaxas1, @impostostaxas2, @impostostaxas3,
                    @videosTitulo, @url1, @url2, @url3, @canaisTitulo, @url4)";
                    command.Parameters.AddWithValue("@conceitosTitulo", educacao.conceitosTitulo);
                    command.Parameters.AddWithValue("@conceitosBasicos", educacao.conceitosBasicos);
                    command.Parameters.AddWithValue("@tiposTitulo", educacao.tiposTitulo);
                    command.Parameters.AddWithValue("@tipoInv1", educacao.tipoInv1);
                    command.Parameters.AddWithValue("@tipoInv2", educacao.tipoInv2);
                    command.Parameters.AddWithValue("@tipoInv3", educacao.tipoInv3);
                    command.Parameters.AddWithValue("@estrategiasTitulo", educacao.estrategiasTitulo);
                    command.Parameters.AddWithValue("@estrategiaInv1", educacao.estrategiaInv1);
                    command.Parameters.AddWithValue("@estrategiaInv2", educacao.estrategiaInv2);
                    command.Parameters.AddWithValue("@estrategiaInv3", educacao.estrategiaInv3);
                    command.Parameters.AddWithValue("@impostosTitulo", educacao.impostosTitulo);
                    command.Parameters.AddWithValue("@impostostaxas1", educacao.impostostaxas1);
                    command.Parameters.AddWithValue("@impostostaxas2", educacao.impostostaxas2);
                    command.Parameters.AddWithValue("@impostostaxas3", educacao.impostostaxas3);
                    command.Parameters.AddWithValue("@videosTitulo", educacao.videosTitulo);
                    command.Parameters.AddWithValue("@url1", educacao.url1);
                    command.Parameters.AddWithValue("@url2", educacao.url2);
                    command.Parameters.AddWithValue("@url3", educacao.url3);
                    command.Parameters.AddWithValue("@canaisTitulo", educacao.canaisTitulo);
                    command.Parameters.AddWithValue("@url4", educacao.url4);

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

        public EducacaoFinanceiraDTO obter()
        {
            List<EducacaoFinanceiraDTO> educacao = new List<EducacaoFinanceiraDTO>();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM EducacaoFinanceira";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            educacao.Add(new EducacaoFinanceiraDTO
                            {
                                id = reader.GetInt32(0),
                                conceitosTitulo = reader.GetString(1),
                                conceitosBasicos = reader.GetString(2),
                                tiposTitulo = reader.GetString(3),
                                tipoInv1 = reader.GetString(4),
                                tipoInv2 = reader.GetString(5),
                                tipoInv3 = reader.GetString(6),
                                estrategiasTitulo = reader.GetString(7),
                                estrategiaInv1 = reader.GetString(8),
                                estrategiaInv2 = reader.GetString(9),
                                estrategiaInv3 = reader.GetString(10),
                                impostosTitulo = reader.GetString(11),
                                impostostaxas1 = reader.GetString(12),
                                impostostaxas2 = reader.GetString(13),
                                impostostaxas3 = reader.GetString(14),
                                videosTitulo = reader.GetString(15),
                                url1 = reader.GetString(16),
                                url2 = reader.GetString(17),
                                url3 = reader.GetString(18),
                                canaisTitulo = reader.GetString(19),
                                url4 = reader.GetString(20),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (educacao.Count == 0)
                return null;
            return educacao[0];
        }
    }
}
