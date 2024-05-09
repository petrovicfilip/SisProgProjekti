using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{
    public class MovieCacheItem
    {
        private List<Movie> _movies;
        private string _url;
        public MovieCacheItem()
        {
            _movies = new List<Movie>();
            _url = string.Empty;
        }
        public MovieCacheItem(List<Movie> movies, string url)
        {
            _movies = movies;
            _url = url;
        }
        public List<Movie> Movies { get { return _movies; } set { _movies = value; } }
        public string Url { get { return _url; } set { _url = value; } }
        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }
    }
}
