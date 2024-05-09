using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{ 
    public class Movie
    {
        private string _title;
        private string _description;
        private string _poster;
        private string _rating;
        private string _release_date;

        public string Title { get { return _title; } set { _title = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Poster { get { return _poster; } set { _poster = value; } }
        public string Rating { get { return _rating; } set { _rating = value; } }
        public string Release_date { get { return _release_date; } set { _release_date = value; } }
        public Movie()
        { 
            _title = string.Empty;
            _description = string.Empty;
            _poster = string.Empty;
            _rating = string.Empty;
            _release_date = string.Empty;
        }
        public Movie(string title, string description, string poster, string rating,string release_date)
        {
            _title = title;
            _description = description;
            _poster = poster;
            _rating = rating;
            _release_date = release_date;
        }


    }
}
