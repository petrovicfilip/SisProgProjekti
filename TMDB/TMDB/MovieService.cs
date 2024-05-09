using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{

    internal class MovieService
    {
        private static readonly string _apiKey = "425f3c1ae557e64fe442891e70cee6a1";
        private static readonly string _baseURL = "https://api.themoviedb.org/3";

        public static List<string> searchMovieService(string query)
        {
            string searchURL = _baseURL + $"/search/movie?query={query}&api_key={_apiKey}";
            List<string> titles = MovieCache.readFromCache(searchURL);

            if(titles.Count == 1 && titles[0] == "~~")
            {
                titles.RemoveAt(0); // brise ~~
                var data = GetData(searchURL);

                if (data == null || data["results"].Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<string>();
                }
                foreach (var movie in data["results"])
                {
                    string title = (string)movie["title"] + " (" + (string)movie["release_date"] + ")";
                    titles.Add(title);
                }
                MovieCache.writeInCache(new MovieCacheItem(titles, searchURL));
            }
            return titles;
        }
        public static List<string> findMovieService(string imdbID)
        {
            string findURL = _baseURL + $"/find/{imdbID}?external_source=imdb_id&api_key={_apiKey}";
            List<string> titles = MovieCache.readFromCache(findURL);

            if (titles.Count == 1 && titles[0] == "~~")
            {
                titles.RemoveAt(0); // brise ~~
                var data = GetData(findURL);
                if (data == null || data["results"].Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<string>();
                }
                foreach (var movie in data["movie_results"])
                {
                    string title = (string)movie["title"] + " (" + (string)movie["release_date"] + ")";
                    titles.Add(title);
                }
                MovieCache.writeInCache(new MovieCacheItem(titles, findURL));
            }
            return titles;
        }
        public static List<string> discoverMoviesService(List<string> parametri)
        {
            string discoverURL = _baseURL + $"/discover/movie?";
            List<string> titles = MovieCache.readFromCache(discoverURL);
            foreach (var param in parametri)
                discoverURL += param + "&";
            discoverURL += $"api_key={_apiKey}";

            if (titles.Count == 1 && titles[0] == "~~")
            {
                titles.RemoveAt(0); // brise ~~
                var data = GetData(discoverURL);
                if (data == null || data["results"].Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<string>();
                }
                foreach (var movie in data["results"])
                {
                    string title = (string)movie["title"] + " (" + (string)movie["release_date"] + ")";
                    titles.Add(title);
                }
                MovieCache.writeInCache(new MovieCacheItem(titles, discoverURL));
            }
            return titles;
        }
        private static JObject GetData(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Greska prilikom rada sa API-em");
                }
                byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;

                if (bytes.Length == 0)
                {
                    throw new Exception($"Dobijen prazan odgovor");
                }

                string responseBody = Encoding.UTF8.GetString(bytes);
                return JObject.Parse(responseBody);
            }
            catch (Exception ex)
            {
                Console.Write($"Greska prilikom izvrsenja: {ex.Message}");
                return new JObject();
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
