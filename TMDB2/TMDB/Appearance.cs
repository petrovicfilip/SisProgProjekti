using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{
    internal class Appearance
    {
        static string VukasinPutanjaDoFajla = "C:\\Users\\vukas\\Desktop\\TMDB2\\SisProgProjekti\\TMDB\\TMDB\\response.txt";
        static string FilipPutanjaDoFajla;
        public static string DrawPage(List<Movie> movies)
        {
  
            StringBuilder forma = new StringBuilder("<div>");

            foreach (var film in movies)
            {
                forma.Append("<div class=\"film\">");
                forma.Append($"<h1>{film.Title} ({film.Release_date}) [{film.Rating}]</h1>");
                forma.Append($"<p>{film.Description}</p>");
                forma.Append($"<img src=\"https://image.tmdb.org/t/p/w500{film.Poster}\" ></img>");
                forma.Append("</div>");
            }

            forma.Append("</div>");


            string htmlContent = File.ReadAllText(VukasinPutanjaDoFajla) + forma.ToString() + @"
                </body>
                </html>";

            // HTTP response header
            string response = "HTTP/1.1 200 OK\r\n" +
                              "Content-Length: " + Encoding.UTF8.GetByteCount(htmlContent) + "\r\n" +
                              "Content-Type: text/html; charset=UTF-8\r\n" +
                              "\r\n" +
                              htmlContent;
            return response; 
        }
    }
}
