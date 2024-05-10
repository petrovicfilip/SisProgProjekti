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

        public static List<Movie> searchMovieService(string query)
        {
            string searchURL = _baseURL + $"/search/movie?query={query}&api_key={_apiKey}";
            List<Movie> titles = MovieCache.readFromCache(searchURL);

            if (titles == null)
            {
                titles = new List<Movie>();

                var data = GetData(searchURL);

                if (data == null || data["results"]?.Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<Movie>();
                }
                foreach (var movie in data["results"])
                {
                    Movie film = new Movie()
                    {
                        Title = (string)movie["title"],
                        Description = (string)movie["overview"],
                        Rating = (string)movie["vote_average"],
                        Release_date = (string)movie["release_date"],
                        Poster = (string)movie["poster_path"]
                    };

                    titles.Add(film);
                }
                MovieCache.writeInCache(new MovieCacheItem(titles, searchURL));
            }
            return titles;
        }
        public static List<Movie> findMovieService(string imdbID)
        {
            string findURL = _baseURL + $"/find/{imdbID}?external_source=imdb_id&api_key={_apiKey}";
            List<Movie>? titles = MovieCache.readFromCache(findURL);

            if (titles == null)
            {
                titles = new List<Movie>();
                var data = GetData(findURL);
                if (data == null || data["movie_results"]!.Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<Movie>();
                }
                foreach (var movie in data["movie_results"])
                {
                    Movie film = new Movie()
                    {
                        Title = (string)movie["title"],
                        Description = (string)movie["overview"],
                        Rating = (string)movie["vote_average"],
                        Release_date = (string)movie["release_date"],
                        Poster = (string)movie["poster_path"]
                    };

                    titles.Add(film);
                }
                MovieCache.writeInCache(new MovieCacheItem(titles, findURL));
            }
            return titles;
        }
        public static List<Movie> discoverMoviesService(string parametri)
        {
            string[] lista_parametri = parametri.Split(' ');
            string discoverURL = _baseURL + $"/discover/movie?";
            List<Movie>? titles = MovieCache.readFromCache(discoverURL);
            foreach (var param in lista_parametri)
                discoverURL += param + "&";
            discoverURL += $"api_key={_apiKey}";

            if (titles == null) 
            {
                titles = new List<Movie>();
                var data = GetData(discoverURL);
                if (data == null || data["results"]!.Count() == 0)
                {
                    Console.WriteLine("Nema dostupnih filmova ili nisu adekvatno pribavljeni.");
                    return new List<Movie>();
                }
                foreach (var movie in data["results"])
                {
                    Movie film = new Movie()
                    {
                        Title = (string)movie["title"],
                        Description = (string)movie["overview"],
                        Rating = (string)movie["vote_average"],
                        Release_date = (string)movie["release_date"],
                        Poster = (string)movie["poster_path"]
                    };

                    titles.Add(film);
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

                Console.WriteLine(response.ToString());

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

               Console.WriteLine(JObject.Parse(responseBody));

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
