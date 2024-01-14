
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Pokeapi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://pokeapi.co/api/v2/pokemon/");
                var result = client.GetAsync(endpoint).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = result.Content.ReadAsStringAsync().Result;
                    var novaConsulta = JsonConvert.DeserializeObject<Pokemons>(json);

                    int count = 1;

                    foreach (var item in novaConsulta.results)
                    {
                        
                        Console.WriteLine($"{count} - {item.name}");
                        count++;
                    }

                    Console.WriteLine("\nEscolha seu Pokemón:\n");
                    int pokemonUser = int.Parse(Console.ReadLine());
                    string pokemonName;
                    
                    count = 1;
                    foreach (var item in novaConsulta.results)
                    {

                        if(count == pokemonUser)
                        {
                            Console.WriteLine($"Você escolheu o {item.name}!");
                            pokemonName = item.name;
                            break;
                        }
                        count++;
                    }
                }
            }
        }

        public class Pokemons
        {

            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public List<GetPokemon> results { get; set; }

        }
        public class GetPokemon
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}