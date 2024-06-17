using Justo.Entities.Entidades;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.DataStorage
{
    public static class ConnectDB
    {
        //tratamento de catch


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
            string StringDeConexaoAtiva = EstabelecerConexao();
            using (var connectionBanco = new SqlConnection(StringDeConexaoAtiva))
            {
                //Advogada, AdvogadaOAB, AdvogadaCPF , Cliente, ClienteCPF , PoloAtivo, PoloPassivo
                connectionBanco.Open();
                string insertQuery = @"
                INSERT INTO Processo (CodPJEC, PJECAcao, Nome , 
                MeioDeComunicacao, MeioDeComunicacaoData, Prazo, ProximoPrazo, ProximoPrazoData,
                UltimaMovimentacaoProcessual, UltimaMovimentacaoProcessualData, AdvogadaCiente, Comarca,
                OrgaoJulgador, Competencia, MotivosProcesso, ValorDaCausa, SegredoJustica, JusGratis, TutelaLiminar,
                Prioridade, Autuacao, TituloProcesso, PartesProcesso, ObsProcesso,
                DataAbertura, DataFim, DataCadastro, CadastradoPor, DataAtualizacao, AtualizadoPor) 

                VALUES (@CodPJEC, @PJECAcao, @Nome ,
                @MeioDeComunicacao, @MeioDeComunicacaoData, @Prazo, @ProximoPrazo, @ProximoPrazoData,
                @UltimaMovimentacaoProcessual, @UltimaMovimentacaoProcessualData, @AdvogadaCiente, @Comarca,
                @OrgaoJulgador, @Competencia, @MotivosProcesso, @ValorDaCausa, @SegredoJustica, @JusGratis, @TutelaLiminar,
                @Prioridade, @Autuacao, @TituloProcesso, @PartesProcesso, @ObsProcesso,
                @DataAbertura, @DataFim, @DataCadastro, @CadastradoPor, @DataAtualizacao, @AtualizadoPor)";

                //@AdvogadaOAB, @Cliente, @ClienteCPF , @AdvogadaCPF , @PoloAtivo, @PoloPassivo
                using (var command = new SqlCommand(insertQuery, connectionBanco))
                {
                    try
                    {


                        // Adicionando os parâmetros de forma segura para evitar SQL Injection
                        command.Parameters.AddWithValue("@CodPJEC", (object)ProcessoInicial.CodPJEC ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PJECAcao", (object)ProcessoInicial.CodPJECAcao ?? DBNull.Value);
                        //command.Parameters.AddWithValue("@Cliente", (object)ProcessoInicial.Cliente ?? DBNull.Value);
                        //command.Parameters.AddWithValue("@ClienteCPF", (object)ProcessoInicial.ClienteCPF ?? DBNull.Value);
                        if(ProcessoInicial.Advogada == null)
                        {
                            ProcessoInicial.Advogada = "Vazio";
                        }
                        command.Parameters.AddWithValue("@Nome", (object)ProcessoInicial.Advogada ?? DBNull.Value);

                        //command.Parameters.AddWithValue("@AdvogadaOAB", (object)ProcessoInicial.AdvogadaOAB ?? DBNull.Value);
                        //command.Parameters.AddWithValue("@AdvogadaCPF", (object)ProcessoInicial.AdvogadaCPF ?? DBNull.Value);
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
                        command.Parameters.AddWithValue("@ValorDaCausa", (object)ProcessoInicial.ValorCausa ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SegredoJustica", (object)ProcessoInicial.SegredoJustica ?? DBNull.Value);
                        command.Parameters.AddWithValue("@JusGratis", (object)ProcessoInicial.JusGratis ?? DBNull.Value);
                        command.Parameters.AddWithValue("@TutelaLiminar", (object)ProcessoInicial.TutelaLiminar ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Prioridade", (object)ProcessoInicial.Prioridade ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Autuacao", (object)ProcessoInicial.Autuacao ?? DBNull.Value);
                        //command.Parameters.AddWithValue("@PoloAtivo", (object)ProcessoInicial.PoloAtivo ?? DBNull.Value);
                        //command.Parameters.AddWithValue("@PoloPassivo", (object)ProcessoInicial.PoloPassivo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@TituloProcesso", (object)ProcessoInicial.TituloProcesso ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PartesProcesso", (object)ProcessoInicial.PartesProcesso ?? DBNull.Value);
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
                    catch (SqlException ex )// Código de erro para violação de chave única/primária
                    {
                        if(ex.Number == 2627)
                        {
                            Console.WriteLine($"Erro: O processo com o CodPJEC:{ProcessoInicial.CodPJEC} fornecido já está cadastrado no banco.");
                        }
                        else
                        {
                            Console.WriteLine($"Erro: O processo com o CodPJEC:{ProcessoInicial.CodPJEC} teve o problema {ex.Message}.");
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

        //vai precisar de um método para verificar o CodPJECAcao pois poderão ter varios diferentes para o mesmo processo.
        //como intimação, procuração e etc e eetc
        public static Processo LerProcesso(string codPJEC)
        {
            if (!string.IsNullOrEmpty(codPJEC))
            {
                string StringConexao = ConnectDB.EstabelecerConexao();
                using (var ConexaoAoBanco = new SqlConnection(StringConexao))
                {
                    string ConsultaQueryProcesso = @"SELECT * FROM Processo WHERE CodPJEC = @codPJEC";
                    using (var ComandoAoBanco = new SqlCommand(ConsultaQueryProcesso, ConexaoAoBanco))
                    {
                        ComandoAoBanco.Parameters.AddWithValue("@codPJEC", codPJEC);

                        try
                        {
                            ConexaoAoBanco.Open();
                            using (var reader = ComandoAoBanco.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Console.WriteLine("Processo encontrado.");
                                    var ProcessoEncontrado = new Processo
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                        CodPJEC = reader.GetString(reader.GetOrdinal("CodPJEC")),
                                        CodPJECAcao = reader.GetString(reader.GetOrdinal("PJECAcao")),
                                        ObsProcesso = reader.IsDBNull(reader.GetOrdinal("ObsProcesso")) ? null : reader.GetString(reader.GetOrdinal("ObsProcesso")),
                                        DataFim_DATETIME = SafeGetDateTime(reader, "DataFim"),
                                        MeioDeComunicacao = reader.IsDBNull(reader.GetOrdinal("MeioDeComunicacao")) ? null : reader.GetString(reader.GetOrdinal("MeioDeComunicacao")),
                                        MeioDeComunicacaoData_DATETIME = SafeGetDateTime(reader, "MeioDeComunicacaoData"),
                                        Prazo = reader.IsDBNull(reader.GetOrdinal("Prazo")) ? null : reader.GetString(reader.GetOrdinal("Prazo")),
                                        ProximoPrazo = reader.IsDBNull(reader.GetOrdinal("ProximoPrazo")) ? null : reader.GetString(reader.GetOrdinal("ProximoPrazo")),
                                        ProximoPrazoData = reader.IsDBNull(reader.GetOrdinal("ProximoPrazoData")) ? null : reader.GetString(reader.GetOrdinal("ProximoPrazoData")),
                                        UltimaMovimentacaoProcessual = reader.IsDBNull(reader.GetOrdinal("UltimaMovimentacaoProcessual")) ? null : reader.GetString(reader.GetOrdinal("UltimaMovimentacaoProcessual")),
                                        UltimaMovimentacaoProcessualData_DATETIME = SafeGetDateTime(reader, "UltimaMovimentacaoProcessualData"),
                                        AdvogadaCiente = reader.IsDBNull(reader.GetOrdinal("AdvogadaCiente")) ? null : reader.GetString(reader.GetOrdinal("AdvogadaCiente")),
                                        Comarca = reader.IsDBNull(reader.GetOrdinal("Comarca")) ? null : reader.GetString(reader.GetOrdinal("Comarca")),
                                        OrgaoJulgador = reader.IsDBNull(reader.GetOrdinal("OrgaoJulgador")) ? null : reader.GetString(reader.GetOrdinal("OrgaoJulgador")),
                                        Competencia = reader.IsDBNull(reader.GetOrdinal("Competencia")) ? null : reader.GetString(reader.GetOrdinal("Competencia")),
                                        MotivosProcesso = reader.IsDBNull(reader.GetOrdinal("MotivosProcesso")) ? null : reader.GetString(reader.GetOrdinal("MotivosProcesso")),
                                        SegredoJustica = reader.IsDBNull(reader.GetOrdinal("SegredoJustica")) ? null : reader.GetString(reader.GetOrdinal("SegredoJustica")),
                                        JusGratis = reader.IsDBNull(reader.GetOrdinal("JusGratis")) ? null : reader.GetString(reader.GetOrdinal("JusGratis")),
                                        TutelaLiminar = reader.IsDBNull(reader.GetOrdinal("TutelaLiminar")) ? null : reader.GetString(reader.GetOrdinal("TutelaLiminar")),
                                        Prioridade = reader.IsDBNull(reader.GetOrdinal("Prioridade")) ? null : reader.GetString(reader.GetOrdinal("Prioridade")),
                                        Autuacao = reader.IsDBNull(reader.GetOrdinal("Autuacao")) ? null : reader.GetString(reader.GetOrdinal("Autuacao")),
                                        TituloProcesso = reader.IsDBNull(reader.GetOrdinal("TituloProcesso")) ? null : reader.GetString(reader.GetOrdinal("TituloProcesso")),
                                        PartesProcesso = reader.IsDBNull(reader.GetOrdinal("PartesProcesso")) ? null : reader.GetString(reader.GetOrdinal("PartesProcesso")),
                                        DataAbertura = SafeGetDateOnly(reader, "DataAbertura"),
                                        AdvogadoId = reader.IsDBNull(reader.GetOrdinal("AdvogadoId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AdvogadoId"))
                                    };
                                    return ProcessoEncontrado;
                                }
                                else
                                {
                                    Console.WriteLine("Processo não encontrado.");
                                    return null;
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"Erro na leitura de ID do processo: {codPJEC} - {ex.Message}");
                            return null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao ler processo: {ex.Message}");
                            return null;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("CODPJEC não preenchido ou está inválido.");
                return null;
            }
        }

        public static void AtualizarProcessoInicial(Processo ProcessoInicial)
        {
            if (!string.IsNullOrEmpty(ProcessoInicial.CodPJEC))
            {
                string StringConexao = ConnectDB.EstabelecerConexao();
                using (var ConexaoAoBanco = new SqlConnection(StringConexao))
                {
                    string QueryAtualizada = @"
                    UPDATE Processo
                    SET
                        Nome = @Nome,
                        CodPJEC = @CodPJEC,
                        PJECAcao = @CodPJECAcao,
                        AdvogadoId = @AdvogadoId,
                        ObsProcesso = @ObsProcesso,
                        DataFim = @DataFim,
                        MeioDeComunicacao = @MeioDeComunicacao,
                        MeioDeComunicacaoData = @MeioDeComunicacaoData,
                        Prazo = @Prazo,
                        ProximoPrazo = @ProximoPrazo,
                        ProximoPrazoData = @ProximoPrazoData,
                        UltimaMovimentacaoProcessual = @UltimaMovimentacaoProcessual,
                        UltimaMovimentacaoProcessualData = @UltimaMovimentacaoProcessualData,
                        AdvogadaCiente = @AdvogadaCiente,
                        Comarca = @Comarca,
                        OrgaoJulgador = @OrgaoJulgador,
                        Competencia = @Competencia,
                        MotivosProcesso = @MotivosProcesso,
                        SegredoJustica = @SegredoJustica,
                        JusGratis = @JusGratis,
                        TutelaLiminar = @TutelaLiminar,
                        Prioridade = @Prioridade,
                        Autuacao = @Autuacao,
                        TituloProcesso = @TituloProcesso,
                        PartesProcesso = @PartesProcesso,
                        DataAbertura = @DataAbertura,
                        ValorDaCausa = @ValorDaCausa,
                        CadastradoPor = @CadastradoPor,
                        DataAtualizacao = @DataAtualizacao,
                        AtualizadoPor = @AtualizadoPor
                    WHERE CodPJEC = @CodPJEC
                    and PJECAcao = @CodPJECAcao";

                    using (var ComandoAoBanco = new SqlCommand(QueryAtualizada, ConexaoAoBanco))
                    {
                        try
                        {
                            // Adicionando os parâmetros de forma segura para evitar SQL Injection
                            ComandoAoBanco.Parameters.AddWithValue("@Nome", (object)ProcessoInicial.Nome ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@AdvogadoId", (object)ProcessoInicial.AdvogadoId ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@ObsProcesso", (object)ProcessoInicial.ObsProcesso ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@CodPJEC", (object)ProcessoInicial.CodPJEC ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@CodPJECAcao", (object)ProcessoInicial.CodPJECAcao ?? DBNull.Value);

                            DateTime? dataFim = ActionsPJE.DateOnlyToDateTime(ProcessoInicial.DataFim);
                            if (dataFim == null || dataFim < SqlDateTime.MinValue.Value)
                            {
                                dataFim = SqlDateTime.MinValue.Value;
                            }
                            ComandoAoBanco.Parameters.AddWithValue("@DataFim", dataFim);

                            ComandoAoBanco.Parameters.AddWithValue("@MeioDeComunicacao", (object)ProcessoInicial.MeioDeComunicacao ?? DBNull.Value);

                            DateTime? meioDeComunicacaoData = ActionsPJE.AjustarDataParaSql(ActionsPJE.StringParaDatetime(ProcessoInicial.MeioDeComunicacaoData));
                            ComandoAoBanco.Parameters.AddWithValue("@MeioDeComunicacaoData", meioDeComunicacaoData);

                            ComandoAoBanco.Parameters.AddWithValue("@Prazo", (object)ProcessoInicial.Prazo ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@ProximoPrazo", (object)ProcessoInicial.ProximoPrazo ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@ProximoPrazoData", (object)ProcessoInicial.ProximoPrazoData ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@UltimaMovimentacaoProcessual", (object)ProcessoInicial.UltimaMovimentacaoProcessual ?? DBNull.Value);

                            DateTime? ultimaMovimentacaoProcessualData = ActionsPJE.AjustarDataParaSql(ActionsPJE.StringParaDatetime(ProcessoInicial.UltimaMovimentacaoProcessualData));
                            ComandoAoBanco.Parameters.AddWithValue("@UltimaMovimentacaoProcessualData", ultimaMovimentacaoProcessualData);

                            ComandoAoBanco.Parameters.AddWithValue("@AdvogadaCiente", (object)ProcessoInicial.AdvogadaCiente ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@Comarca", (object)ProcessoInicial.Comarca ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@OrgaoJulgador", (object)ProcessoInicial.OrgaoJulgador ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@Competencia", (object)ProcessoInicial.Competencia ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@MotivosProcesso", (object)ProcessoInicial.MotivosProcesso ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@ValorDaCausa", (object)ProcessoInicial.ValorCausa ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@SegredoJustica", (object)ProcessoInicial.SegredoJustica ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@JusGratis", (object)ProcessoInicial.JusGratis ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@TutelaLiminar", (object)ProcessoInicial.TutelaLiminar ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@Prioridade", (object)ProcessoInicial.Prioridade ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@Autuacao", (object)ProcessoInicial.Autuacao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@TituloProcesso", (object)ProcessoInicial.TituloProcesso ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@PartesProcesso", (object)ProcessoInicial.PartesProcesso ?? DBNull.Value);

                            DateTime? dataAbertura = ActionsPJE.DateOnlyToDateTime(ProcessoInicial.DataAbertura);
                            if (dataAbertura == null || dataAbertura < SqlDateTime.MinValue.Value)
                            {
                                dataAbertura = SqlDateTime.MinValue.Value;
                            }
                            ComandoAoBanco.Parameters.AddWithValue("@DataAbertura", dataAbertura);

                            ComandoAoBanco.Parameters.AddWithValue("@CadastradoPor", (object)ProcessoInicial.CadastradoPor ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@DataAtualizacao", (object)ProcessoInicial.DataAtualizacao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@AtualizadoPor", (object)ProcessoInicial.AtualizadoPor ?? DBNull.Value);

                            ConexaoAoBanco.Open();
                            int result = ComandoAoBanco.ExecuteNonQuery();

                            if (result > 0)
                            {
                                Console.WriteLine("Processo atualizado com sucesso.");
                            }
                            else
                            {
                                Console.WriteLine("Falha ao atualizar o processo.");
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627) // Código de erro para violação de chave única/primária
                            {
                                Console.WriteLine($"Erro: O processo com o CodPJEC:{ProcessoInicial.CodPJEC} fornecido já está cadastrado no banco.");
                            }
                            else
                            {
                                Console.WriteLine($"Erro: O processo com o CodPJEC:{ProcessoInicial.CodPJEC} teve o problema {ex.Message}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao adicionar parâmetro: " + ex.Message);
                            throw;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("CODPJEC não preenchido ou está inválido.");
            }
        }

        //testando lista agora:
        public static void SalvarProcessoMovimentacaoProcessual(List<ProcessoAtualizacao> listaProcessosMovimentacao)
        {
            if (listaProcessosMovimentacao == null || listaProcessosMovimentacao.Count == 0)
            {
                Console.WriteLine("Lista de processos movimentação está vazia ou inválida.");
                return;
            }

            string StringConexao = ConnectDB.EstabelecerConexao();
            using (var ConexaoAoBanco = new SqlConnection(StringConexao))
            {
                ConexaoAoBanco.Open();

                // Verifica se o ProcessoId existe na tabela Processo
                var processoMovimentacao = listaProcessosMovimentacao[0]; // Assume que todos os itens têm o mesmo ProcessoId
                string VerificaProcessoQuery = "SELECT COUNT(1) FROM Processo WHERE Id = @ProcessoId";
                using (var ComandoVerificaProcesso = new SqlCommand(VerificaProcessoQuery, ConexaoAoBanco))
                {
                    ComandoVerificaProcesso.Parameters.AddWithValue("@ProcessoId", (object)processoMovimentacao.ProcessoId ?? DBNull.Value);
                    int count = (int)ComandoVerificaProcesso.ExecuteScalar();
                    if (count == 0)
                    {
                        Console.WriteLine($"Erro: O ProcessoId {processoMovimentacao.ProcessoId} não existe na tabela Processo.");
                        return;
                    }
                    Console.WriteLine($"Processo {processoMovimentacao.ProcessoId} existe e foi localizado, verificando se existem registros duplicados.");
                }

                // Verifica e apaga registros duplicados uma vez
                string VerificaDuplicadoQuery = @"
                SELECT COUNT(1) 
                FROM ProcessosAtualizacao 
                WHERE ProcessoId = @ProcessoId 
                AND CodPJEC = @CodPJEC
                AND PJECAcao = @PJECAcao";
                using (var ComandoVerificaDuplicado = new SqlCommand(VerificaDuplicadoQuery, ConexaoAoBanco))
                {
                    ComandoVerificaDuplicado.Parameters.AddWithValue("@ProcessoId", (object)processoMovimentacao.ProcessoId ?? DBNull.Value);
                    ComandoVerificaDuplicado.Parameters.AddWithValue("@CodPJEC", (object)processoMovimentacao.CodPJEC ?? DBNull.Value);
                    ComandoVerificaDuplicado.Parameters.AddWithValue("@PJECAcao", (object)processoMovimentacao.PJECAcao ?? DBNull.Value);

                    int count = (int)ComandoVerificaDuplicado.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine($"Apagando registros anteriores de: {processoMovimentacao.ProcessoId}, CodPJEC {processoMovimentacao.CodPJEC} e PJECAcao {processoMovimentacao.PJECAcao}. e inserindo novas atualizações.");
                        string DeleteQuery = @"
                        DELETE FROM ProcessosAtualizacao 
                        WHERE ProcessoId = @ProcessoId 
                        AND CodPJEC = @CodPJEC
                        AND PJECAcao = @PJECAcao";
                        using (var ComandoDelete = new SqlCommand(DeleteQuery, ConexaoAoBanco))
                        {
                            ComandoDelete.Parameters.AddWithValue("@ProcessoId", (object)processoMovimentacao.ProcessoId ?? DBNull.Value);
                            ComandoDelete.Parameters.AddWithValue("@CodPJEC", (object)processoMovimentacao.CodPJEC ?? DBNull.Value);
                            ComandoDelete.Parameters.AddWithValue("@PJECAcao", (object)processoMovimentacao.PJECAcao ?? DBNull.Value);
                            ComandoDelete.ExecuteNonQuery();
                        }
                    }
                }

                // Inserir todas as movimentações na lista
                foreach (var movimentacao in listaProcessosMovimentacao)
                {
                    string QueryMovimentacao = @"
                INSERT INTO ProcessosAtualizacao (
                    ProcessoId,
                    CodPJEC,
                    PJECAcao,
                    ConteudoAtualizacao, 
                    TituloMovimento, 
                    DataMovimentacao, 
                    Nome,
                    CadastradoPor,
                    DataCadastro, 
                    DataAtualizacao, 
                    AtualizadoPor
                ) VALUES (
                    @ProcessoId,
                    @CodPJEC,
                    @PJECAcao,
                    @ConteudoAtualizacao, 
                    @TituloMovimento, 
                    @DataMovimentacao, 
                    @Nome,
                    @CadastradoPor, 
                    @DataCadastro, 
                    @DataAtualizacao, 
                    @AtualizadoPor
                )";

                    using (var ComandoAoBanco = new SqlCommand(QueryMovimentacao, ConexaoAoBanco))
                    {
                        try
                        {
                            // Adicionando os parâmetros de forma segura para evitar SQL Injection
                            ComandoAoBanco.Parameters.AddWithValue("@ProcessoId", (object)movimentacao.ProcessoId ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@CodPJEC", (object)movimentacao.CodPJEC ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@PJECAcao", (object)movimentacao.PJECAcao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@ConteudoAtualizacao", (object)movimentacao.ConteudoAtualizacao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@TituloMovimento", (object)movimentacao.TituloMovimento ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@DataMovimentacao", (object)movimentacao.DataMovimentacao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@Nome", (object)movimentacao.Nome ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@CadastradoPor", (object)movimentacao.CadastradoPor ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@DataCadastro", (object)movimentacao.DataCadastro ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@DataAtualizacao", (object)movimentacao.DataAtualizacao ?? DBNull.Value);
                            ComandoAoBanco.Parameters.AddWithValue("@AtualizadoPor", (object)movimentacao.AtualizadoPor ?? DBNull.Value);

                            int result = ComandoAoBanco.ExecuteNonQuery();
                            if (result > 0)
                            {
                                Console.WriteLine($"ProcessoAtualizacao {movimentacao.ProcessoId} inserido com sucesso.");
                            }
                            else
                            {
                                Console.WriteLine($"Falha ao inserir ProcessoAtualizacao {movimentacao.ProcessoId}.");
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627) // Código de erro para violação de chave única/primária
                            {
                                Console.WriteLine($"Erro: O processo com o CodPJEC:{movimentacao.CodPJEC} não pode ser atualizado.");
                            }
                            else
                            {
                                Console.WriteLine($"Erro: O processo com o CodPJEC:{movimentacao.CodPJEC} teve o problema {ex.Message}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao adicionar parâmetro: " + ex.Message);
                            throw;
                        }
                    }
                }
            }
        }
        //será inserido no final da carga ao abrir aquele menu lateral de cima com os
        //dados do processo ( que demorei a ver que fica escondido ).
        public static Advogado LerAdvogado(int codADVOGADO)
        {
            if (codADVOGADO >= 0)
            {
                string StringDeConexaoAtiva = EstabelecerConexao();

                using (var connectionBanco = new SqlConnection(StringDeConexaoAtiva))
                {
                    string LeituraQueryAdvogado = @"SELECT * FROM Advogado WHERE id = @codAdvogado";
                    using (var command = new SqlCommand(LeituraQueryAdvogado, connectionBanco))
                    {
                        command.Parameters.AddWithValue("@codAdvogado", codADVOGADO);

                        try
                        {
                            connectionBanco.Open();
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Console.WriteLine("Advogado encontrado.");
                                    var AdvogadoEncontrado = new Advogado
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                        Oab = reader.GetString(reader.GetOrdinal("Oab")),
                                        Cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                        // Adicione os outros campos conforme necessário
                                    };
                                    return AdvogadoEncontrado;
                                }
                                else
                                {
                                    Console.WriteLine("Advogado não encontrado.");
                                    return null;
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"Erro na leitura de ID de advogado: {codADVOGADO} - {ex.Message}");
                            return null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao ler advogado: {ex.Message}");
                            return null;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ID de advogado inválido.");
                return null;
            }
        }

        public static void InserirAdvogados(Advogado AdvogadoaSerInserido)
        {
            string StringDeConexaoAtiva = EstabelecerConexao();
            using (var connectionBanco = new SqlConnection(StringDeConexaoAtiva))
            {
                connectionBanco.Open();
                string querysqlverificaadvogado = @"select COUNT(1) from Processo where Id = @AdvogadoId";
                using (var ComandoVerificaProcesso = new SqlCommand(querysqlverificaadvogado, connectionBanco))
                {
                    //esta errado
                    ComandoVerificaProcesso.Parameters.AddWithValue("@AdvogadoId", (object)AdvogadoaSerInserido.Cpf ?? DBNull.Value);
                    int count = (int)ComandoVerificaProcesso.ExecuteScalar();
                    if (count == 0)
                    {
                        Console.WriteLine($"Erro: Advogado {AdvogadoaSerInserido.Id} não existe na tabela Advogado.");
                        botar aqui para adicionar novos advogados.
                        //return;
                    }
                    else
                    {
                        Console.WriteLine($"Advogado {AdvogadoaSerInserido.Nome} já está cadastrado no banco de dados.");
                        return;
                    }

                }
            }
        }
        public static void InserirPolosPartes(List<Polo> ProcessoComPolo)
        {
            string StringDeConexaoAtiva = EstabelecerConexao();
            using (var connectionBanco = new SqlConnection(StringDeConexaoAtiva))
            {
                connectionBanco.Open();
                using (var transaction = connectionBanco.BeginTransaction())
                {
                    try
                    {
                        foreach (var polo in ProcessoComPolo)
                        {
                            // Verificar se o registro já existe
                            string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM Polo 
                    WHERE ProcessoId = @ProcessoId AND NomeParte = @NomeParte AND TipoParte = @TipoParte";
                            using (var checkCommand = new SqlCommand(checkQuery, connectionBanco, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@ProcessoId", polo.ProcessoId);
                                checkCommand.Parameters.AddWithValue("@NomeParte", polo.NomeParte);
                                checkCommand.Parameters.AddWithValue("@TipoParte", polo.TipoParte);
                                int count = (int)checkCommand.ExecuteScalar();

                                if (count > 0)
                                {
                                    // Registro existe, fazer UPDATE
                                    string updateQuery = @"
                            UPDATE Polo
                            SET
                                CPFCNPJParte = @CPFCNPJParte,
                                NomeAdvogado = @NomeAdvogado,
                                CPFAdvogado = @CPFAdvogado,
                                OAB = @OAB,
                                Nome = @Nome,
                                DataCadastro = @DataCadastro,
                                CadastradoPor = @CadastradoPor,
                                DataAtualizacao = @DataAtualizacao,
                                AtualizadoPor = @AtualizadoPor
                            WHERE ProcessoId = @ProcessoId AND NomeParte = @NomeParte AND TipoParte = @TipoParte";
                                    using (var updateCommand = new SqlCommand(updateQuery, connectionBanco, transaction))
                                    {
                                        updateCommand.Parameters.AddWithValue("@ProcessoId", polo.ProcessoId);
                                        updateCommand.Parameters.AddWithValue("@NomeParte", polo.NomeParte);
                                        updateCommand.Parameters.AddWithValue("@TipoParte", polo.TipoParte);
                                        updateCommand.Parameters.AddWithValue("@CPFCNPJParte", polo.CPFCNPJParte);
                                        updateCommand.Parameters.AddWithValue("@NomeAdvogado", polo.NomeAdvogado);
                                        updateCommand.Parameters.AddWithValue("@CPFAdvogado", polo.CPFAdvogado);
                                        updateCommand.Parameters.AddWithValue("@OAB", polo.OAB);
                                        updateCommand.Parameters.AddWithValue("@Nome", polo.Nome);
                                        updateCommand.Parameters.AddWithValue("@DataCadastro", polo.DataCadastro);
                                        updateCommand.Parameters.AddWithValue("@CadastradoPor", polo.CadastradoPor);
                                        updateCommand.Parameters.AddWithValue("@DataAtualizacao", polo.DataAtualizacao);
                                        updateCommand.Parameters.AddWithValue("@AtualizadoPor", polo.AtualizadoPor);

                                        updateCommand.ExecuteNonQuery();
                                        Console.WriteLine($"Polo atualizado com sucesso.");
                                    }
                                }
                                else
                                {
                                    // Registro não existe, fazer INSERT
                                    string insertQuery = @"
                            INSERT INTO Polo (
                                ProcessoId, NomeParte, TipoParte, 
                                CPFCNPJParte, NomeAdvogado, CPFAdvogado, OAB, Nome, DataCadastro, CadastradoPor, DataAtualizacao, AtualizadoPor)
                            VALUES (
                                @ProcessoId, @NomeParte, @TipoParte, 
                                @CPFCNPJParte, @NomeAdvogado, @CPFAdvogado, @OAB, @Nome, @DataCadastro, @CadastradoPor, @DataAtualizacao, @AtualizadoPor)";
                                    using (var insertCommand = new SqlCommand(insertQuery, connectionBanco, transaction))
                                    {
                                        insertCommand.Parameters.AddWithValue("@ProcessoId", polo.ProcessoId);
                                        insertCommand.Parameters.AddWithValue("@NomeParte", polo.NomeParte);
                                        insertCommand.Parameters.AddWithValue("@TipoParte", polo.TipoParte);
                                        insertCommand.Parameters.AddWithValue("@CPFCNPJParte", polo.CPFCNPJParte);
                                        insertCommand.Parameters.AddWithValue("@NomeAdvogado", polo.NomeAdvogado);
                                        insertCommand.Parameters.AddWithValue("@CPFAdvogado", polo.CPFAdvogado);
                                        insertCommand.Parameters.AddWithValue("@OAB", polo.OAB);
                                        insertCommand.Parameters.AddWithValue("@Nome", polo.Nome);
                                        insertCommand.Parameters.AddWithValue("@DataCadastro", polo.DataCadastro);
                                        insertCommand.Parameters.AddWithValue("@CadastradoPor", polo.CadastradoPor);
                                        insertCommand.Parameters.AddWithValue("@DataAtualizacao", polo.DataAtualizacao);
                                        insertCommand.Parameters.AddWithValue("@AtualizadoPor", polo.AtualizadoPor);

                                        insertCommand.ExecuteNonQuery();
                                        Console.WriteLine($"Polo inserido com sucesso.");
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Number == 2627) // Código de erro para violação de chave única/primária
                        {
                            Console.WriteLine($"Erro: Polo nao pode ser inserido: {ex.Message}.");
                        }
                        else
                        {
                            Console.WriteLine($"Erro: O Polo teve o problema {ex.Message}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Erro ao adicionar parâmetro: " + ex.Message);
                        throw;
                    }
                }
            }
        }

        private static DateTime? SafeGetDateTime(SqlDataReader reader, string columnName)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                    return null;

                var value = reader.GetValue(reader.GetOrdinal(columnName));

                if (value is DateTime dateTime)
                    return dateTime;

                if (value is string dateString)
                {
                    if (string.IsNullOrWhiteSpace(dateString))
                        return null;

                    if (DateTime.TryParse(dateString, out dateTime))
                        return dateTime;
                    else
                        Console.WriteLine($"Erro ao converter o campo '{columnName}' para DateTime: valor '{dateString}' inválido.");
                }

                return null;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Erro ao converter o campo '{columnName}' para DateTime: {ex.Message}");
                return null;
            }
        }

        private static DateOnly SafeGetDateOnly(SqlDataReader reader, string columnName)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                    return DateOnly.FromDateTime(DateTime.MinValue);

                var value = reader.GetValue(reader.GetOrdinal(columnName));

                if (value is DateTime dateTime)
                    return DateOnly.FromDateTime(dateTime);

                if (value is string dateString)
                {
                    if (string.IsNullOrWhiteSpace(dateString))
                        return DateOnly.FromDateTime(DateTime.MinValue);

                    if (DateTime.TryParse(dateString, out dateTime))
                        return DateOnly.FromDateTime(dateTime);
                    else
                        Console.WriteLine($"Erro ao converter o campo '{columnName}' para DateOnly: valor '{dateString}' inválido.");
                }

                return DateOnly.FromDateTime(DateTime.MinValue);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Erro ao converter o campo '{columnName}' para DateOnly: {ex.Message}");
                return DateOnly.FromDateTime(DateTime.MinValue);
            }
        }
    }
        //salvar movimentação processual aqui,  fazendo inserts nas respectivas tabelas para isso.
}
