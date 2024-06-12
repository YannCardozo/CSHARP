﻿using Pje_WebScrapping.Actions;
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
        //tratamento de catch 
        public static SqlException ExceptionDoBanco;


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
    }
        //salvar movimentação processual aqui,  fazendo inserts nas respectivas tabelas para isso.
}
