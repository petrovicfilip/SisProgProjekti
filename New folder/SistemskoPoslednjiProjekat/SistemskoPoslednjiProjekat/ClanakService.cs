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
        private readonly string apiKey = "apikey";
        private readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<Clanak>> FetchClanciAsync(string query)
        {

            var url = $"https://newsapi.org/v2/everything?q={Uri.EscapeDataString(query)}&sortBy=popularity&apiKey={apiKey}";
            Console.WriteLine($"Fetching URL: {url}");

            //Mora inace se ucek vraca 400 status code
            client.DefaultRequestHeaders.Add("User-Agent", "SistemskoPoslednjiProjekat/1.0");

            try
            {
                var response = await client.GetAsync(url);
                Console.WriteLine($"Response Status Code: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {errorContent}");
                    response.EnsureSuccessStatusCode();
                }

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
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
                return Enumerable.Empty<Clanak>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return Enumerable.Empty<Clanak>();
            }
        }
    }
}