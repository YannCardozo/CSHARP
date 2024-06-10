using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.DataStorage
{
    public static class ConnectDB
    {
        public static string connectionStringCasa = "Server=DESKTOP-RAGDGN0\\SQLEXPRESS;Database=JustoTesteValdir;Trusted_Connection=True;TrustServerCertificate=true;";
        public static string connectionStringTrabalho = "Server=DESKTOP-O22D6C8\\SQLEXPRESS;Database=JustoTesteValdir;Trusted_Connection=True;TrustServerCertificate=true;";
        public static string connectionStringNotebook = "Server=YANN-GALAXYBOOK\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=true;";


        public static string EstabelecerConexao()
        {
            string[] connectionStrings = { connectionStringCasa, connectionStringTrabalho, connectionStringNotebook };

            foreach (var connectionString in connectionStrings)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        return connectionString;
                    }
                    catch (SqlException)
                    {
                        // Não faz nada, apenas tenta a próxima string de conexão
                    }
                }
            }

            throw new InvalidOperationException("Nenhuma string de conexão válida encontrada. - Verficar ConnectDB");
        }
        public static void SalvarProcessoInicial(Processo ProcessoInicial)
        {
            try
            {
                string StringDeConexaoAtiva = EstabelecerConexao();
                using (var connectionBanco = new SqlConnection(StringDeConexaoAtiva))
                {
                    connectionBanco.Open();
                    string insertQuery = @"
                    INSERT INTO Processo (CodPJEC, CodPJECAcao, Cliente, ClienteCPF, Advogada, AdvogadaOAB, AdvogadaCPF,
                    MeioDeComunicacao, MeioDeComunicacaoData, Prazo, ProximoPrazo, ProximoPrazoData,
                    UltimaMovimentacaoProcessual, UltimaMovimentacaoProcessualData, AdvogadaCiente, Comarca,
                    OrgaoJulgador, Competencia, MotivosProcesso, ValorCausa, SegredoJustica, JusGratis, TutelaLiminar,
                    Prioridade, Autuacao, PoloAtivo, PoloPassivo, TituloProcesso, PartesProcesso, ComarcaInicial, ObsProcesso,
                    DataAbertura, DataFim, DataCadastro, CadastradoPor, DataAtualizacao, AtualizadoPor) 

                    VALUES (@CodPJEC, @CodPJECAcao, @Cliente, @ClienteCPF, @Advogada, @AdvogadaOAB, @AdvogadaCPF,
                    @MeioDeComunicacao, @MeioDeComunicacaoData, @Prazo, @ProximoPrazo, @ProximoPrazoData,
                    @UltimaMovimentacaoProcessual, @UltimaMovimentacaoProcessualData, @AdvogadaCiente, @Comarca,
                    @OrgaoJulgador, @Competencia, @MotivosProcesso, @ValorCausa, @SegredoJustica, @JusGratis, @TutelaLiminar,
                    @Prioridade, @Autuacao, @PoloAtivo, @PoloPassivo, @TituloProcesso, @PartesProcesso, @ComarcaInicial, @ObsProcesso,
                    @DataAbertura, @DataFim, @DataCadastro, @CadastradoPor, @DataAtualizacao, @AtualizadoPor)";
                    using (var command = new SqlCommand(insertQuery, connectionBanco))
                    {
                        try
                        {


                            // Adicionando os parâmetros de forma segura para evitar SQL Injection
                            command.Parameters.AddWithValue("@CodPJEC", (object)ProcessoInicial.CodPJEC ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CodPJECAcao", (object)ProcessoInicial.CodPJECAcao ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Cliente", (object)ProcessoInicial.Cliente ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ClienteCPF", (object)ProcessoInicial.ClienteCPF ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Advogada", (object)ProcessoInicial.Advogada ?? DBNull.Value);
                            command.Parameters.AddWithValue("@AdvogadaOAB", (object)ProcessoInicial.AdvogadaOAB ?? DBNull.Value);
                            command.Parameters.AddWithValue("@AdvogadaCPF", (object)ProcessoInicial.AdvogadaCPF ?? DBNull.Value);
                            command.Parameters.AddWithValue("@MeioDeComunicacao", (object)ProcessoInicial.MeioDeComunicacao ?? DBNull.Value);


                            DateTime? meioDeComunicacaoData = ActionsPJE.AjustarDataParaSql(ActionsPJE.StringParaDatetime(ProcessoInicial.MeioDeComunicacaoData));
                            Console.WriteLine($"MeioDeComunicacaoData ajustada: {meioDeComunicacaoData}");
                            command.Parameters.AddWithValue("@MeioDeComunicacaoData", (object)meioDeComunicacaoData ?? DBNull.Value);

                            command.Parameters.AddWithValue("@Prazo", (object)ProcessoInicial.Prazo ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ProximoPrazo", (object)ProcessoInicial.ProximoPrazo ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ProximoPrazoData", (object)ProcessoInicial.ProximoPrazoData ?? DBNull.Value);
                            command.Parameters.AddWithValue("@UltimaMovimentacaoProcessual", (object)ProcessoInicial.UltimaMovimentacaoProcessual ?? DBNull.Value);




                            DateTime? ultimaMovimentacaoProcessualData = ActionsPJE.AjustarDataParaSql(ActionsPJE.StringParaDatetime(ProcessoInicial.UltimaMovimentacaoProcessualData));
                            Console.WriteLine($"UltimaMovimentacaoProcessualData ajustada: {ultimaMovimentacaoProcessualData}");
                            command.Parameters.AddWithValue("@UltimaMovimentacaoProcessualData", (object)ultimaMovimentacaoProcessualData ?? DBNull.Value);




                            command.Parameters.AddWithValue("@AdvogadaCiente", (object)ProcessoInicial.AdvogadaCiente ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Comarca", (object)ProcessoInicial.Comarca ?? DBNull.Value);
                            command.Parameters.AddWithValue("@OrgaoJulgador", (object)ProcessoInicial.OrgaoJulgador ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Competencia", (object)ProcessoInicial.Competencia ?? DBNull.Value);
                            command.Parameters.AddWithValue("@MotivosProcesso", (object)ProcessoInicial.MotivosProcesso ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ValorCausa", (object)ProcessoInicial.ValorCausa ?? DBNull.Value);
                            command.Parameters.AddWithValue("@SegredoJustica", (object)ProcessoInicial.SegredoJustica ?? DBNull.Value);
                            command.Parameters.AddWithValue("@JusGratis", (object)ProcessoInicial.JusGratis ?? DBNull.Value);
                            command.Parameters.AddWithValue("@TutelaLiminar", (object)ProcessoInicial.TutelaLiminar ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Prioridade", (object)ProcessoInicial.Prioridade ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Autuacao", (object)ProcessoInicial.Autuacao ?? DBNull.Value);
                            command.Parameters.AddWithValue("@PoloAtivo", (object)ProcessoInicial.PoloAtivo ?? DBNull.Value);
                            command.Parameters.AddWithValue("@PoloPassivo", (object)ProcessoInicial.PoloPassivo ?? DBNull.Value);
                            command.Parameters.AddWithValue("@TituloProcesso", (object)ProcessoInicial.TituloProcesso ?? DBNull.Value);
                            command.Parameters.AddWithValue("@PartesProcesso", (object)ProcessoInicial.PartesProcesso ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ComarcaInicial", (object)ProcessoInicial.ComarcaInicial ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ObsProcesso", (object)ProcessoInicial.ObsProcesso ?? DBNull.Value);


                            DateTime? dataAbertura = ActionsPJE.DateOnlyToDateTime(ProcessoInicial.DataAbertura);
                            if (dataAbertura == null || dataAbertura < SqlDateTime.MinValue.Value)
                            {
                                dataAbertura = SqlDateTime.MinValue.Value;
                            }
                            Console.WriteLine($"data abertura: {dataAbertura}");
                            command.Parameters.AddWithValue("@DataAbertura", (object)dataAbertura ?? DBNull.Value);
                            command.Parameters.AddWithValue("@DataCadastro", (object)dataAbertura ?? DBNull.Value);

                            DateTime? dataFim = ActionsPJE.DateOnlyToDateTime(ProcessoInicial.DataFim);
                            if (dataFim == null || dataFim < SqlDateTime.MinValue.Value)
                            {
                                dataFim = SqlDateTime.MinValue.Value;
                            }
                            Console.WriteLine($"data fim: {dataFim}");
                            command.Parameters.AddWithValue("@DataFim", (object)dataFim ?? DBNull.Value);

                            command.Parameters.AddWithValue("@CadastradoPor", (object)ProcessoInicial.CadastradoPor ?? DBNull.Value);
                            command.Parameters.AddWithValue("@DataAtualizacao", (object)ProcessoInicial.DataAtualizacao ?? DBNull.Value);
                            command.Parameters.AddWithValue("@AtualizadoPor", (object)ProcessoInicial.AtualizadoPor ?? DBNull.Value);

                            // Executa o comando de inserção e retorna o número de linhas afetadas
                            int result = command.ExecuteNonQuery();

                            // Verifica se a inserção foi bem-sucedida
                            if (result > 0)
                            {
                                Console.WriteLine("Processo inserido com sucesso.");
                            }
                            else
                            {
                                Console.WriteLine("Falha ao inserir o processo.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao adicionar parâmetro: " + ex.Message );
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir o processo: " + ex.Message);
            }
        }

        
    }
}
