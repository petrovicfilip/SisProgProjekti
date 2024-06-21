using System;

namespace SistemskoPoslednjiProjekat
{
    public class Clanak
    {
        public string Author { get; set; }
        public string Tilte { get; set; }
        public string Description { get; set; }
        public string UrlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string Content { get; set; }
    }
}