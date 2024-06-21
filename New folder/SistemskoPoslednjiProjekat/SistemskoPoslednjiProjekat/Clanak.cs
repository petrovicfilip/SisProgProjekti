using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemskoPoslednjiProjekat
{
    public class Clanak
    {
        public string Author {  get; set; }
        public string Tilte {  get; set; }
        public string Description {  get; set; }
        public string UrlToImage {  get; set; }
        public DateTime publishedAt { get; set; }
        public string Content {  get; set; }
    }
}
