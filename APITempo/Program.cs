using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Npgsql;

namespace APITempo;

public class Program
{
    public static void Main(String[] args)
    {
        NpgsqlCommand cmd = new NpgsqlCommand();//objeto onde vamos inserir o comando SQL
        Conexao conexao = new Conexao(); //objeto para conexao com banco
        string mensagem = "";
        string cidade = "";

        Console.WriteLine("DIgite o CEP a ser consultado");
        string cepProcurado = Console.ReadLine();

        string strCEP = $"https://viacep.com.br/ws/{cepProcurado}/json/";

        using (HttpClient client1 = new HttpClient())
        {
            try { 
                var response = client1.GetAsync(strCEP).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    CepResponse cepResponse = JsonConvert.DeserializeObject<CepResponse>(result);
                    Console.WriteLine($"CEP: {cepResponse.cep}\n" +
                        $"Logradouro: {cepResponse.logradouro}\n" +
                        $"Bairro: {cepResponse.bairro}\n" +
                        $"Cidade: {cepResponse.localidade}\n" +
                        $"UF: {cepResponse.uf}\n" +
                        $"DDD: {cepResponse.ddd}");
                    cidade = $"{cepResponse.localidade},{cepResponse.uf}";
                    cidade = cidade.Replace(' ', '-');
                    //Console.WriteLine(cidade);
                    string cep = cepResponse.cep;
                    string logradouro = cepResponse.logradouro;
                    string bairro = cepResponse.bairro;
                    string city = cepResponse.localidade;
                    string estado = cepResponse.uf;

                    cmd.CommandText = "insert into ender(cep, rua, bairro, cidade, estado) values (@cep, @rua, @bairro, @cidade, @estado)";//vamos passar o sql


                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@rua", logradouro);
                    cmd.Parameters.AddWithValue("@bairro", bairro);
                    cmd.Parameters.AddWithValue("@cidade", city);
                    cmd.Parameters.AddWithValue("@estado", estado);


                    try//conexao com o banco
                    {
                        cmd.Connection = conexao.conectar();//abre conexao com o banco
                        cmd.ExecuteNonQuery();//enviar a query
                        conexao.desconcetar(); //desconectar
                        mensagem = "Cadastrado com sucesso";
                    }catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e) { 
                Console.WriteLine(e);
            }
        }

          string strUrl = $"https://api.hgbrasil.com/weather?key=58fe454a&city_name={cidade}";


          using (HttpClient client = new HttpClient())
          {
              try
              {            
                  var response = client.GetAsync(strUrl).Result;
                  if (response.IsSuccessStatusCode)
                  {
                      var result = response.Content.ReadAsStringAsync().Result;
                      Previsao previsao = JsonConvert.DeserializeObject<Previsao>(result);
                      Console.WriteLine($"\nPrevisao do tempo para {previsao.tempo.city}");
                      Console.WriteLine($"Temperatura: {previsao.tempo.temperatura}°\n" +
                          $"Humidade: {previsao.tempo.humidity}%" +
                          $"\nVelocidade do Vento: {previsao.tempo.wind_speedy}" +
                          $"\nNascer do Sol: {previsao.tempo.sunrise}" +
                          $"\nPor do Sol: {previsao.tempo.sunset}" +
                          $"\nData: {previsao.tempo.data}" +
                          $"\nHora: {previsao.tempo.hora}\n" +
                          $"Descricao: {previsao.tempo.description}");
                    Console.WriteLine("\nCondição para os próximos 3 dias\n");
                    for(int i = 1; i < 4; i++)
                    {
                        Console.WriteLine($"Dia: {previsao.tempo.diasSeguintes[i].diaDaSemana} - {previsao.tempo.diasSeguintes[i].data}\n" +
                            $"Maxima: {previsao.tempo.diasSeguintes[i].max}º\n" +
                            $"Minima: {previsao.tempo.diasSeguintes[i].min}º\n" +
                            $"Descrição: {previsao.tempo.diasSeguintes[i].description}\n" +
                            $"Condição: {previsao.tempo.diasSeguintes[i].condition}\n\n");

                    }

              }

              }catch (Exception e)
              {
                  Console.WriteLine(e);
              }

          }
    }
}