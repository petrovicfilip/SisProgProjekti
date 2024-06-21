using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SistemskoPoslednjiProjekat
{
    public class ClanakService
    {
        private readonly string _apiKey = "d8be50c5f1ea4d0392a1bbbd90db3eb7";

        public async Task<IEnumerable<Clanak>> FetchClanciAsync(string query)
        {
            HttpClient _client = new HttpClient();
            var url = $"https://newsapi.org/v2/everything?q={query}&sortBy=popularity&apiKey={_apiKey}";
            Console.WriteLine(url);
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var jsonResponse = JObject.Parse(content);
            var articlesJson = jsonResponse["articles"];

            if (articlesJson == null)
            {
                return Enumerable.Empty<Clanak>();
            }

            return articlesJson.Select(article => new Clanak
            {
                Author = (string)article["author"],
                Tilte = (string)article["title"],
                Description = (string)article["description"],
                UrlToImage = (string)article["urlToImage"],
                publishedAt = (DateTime)article["publishedAt"],
                Content = (string)article["content"]
            });
        }
    }
}
