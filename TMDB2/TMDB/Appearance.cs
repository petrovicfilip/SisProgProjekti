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
        static string FilipPutanjaDoFajla = "C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB2\\TMDB\\response.txt";

        static string badResponseFilip = "C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB2\\TMDB\\badresponse.txt";
        public static string DrawPage(List<Movie> movies)
        {
  
            StringBuilder forma = new StringBuilder("<div>");
            string response = "";

            if (movies.Count == 0)
            {
                string errorHtmlContent = File.ReadAllText(badResponseFilip);

                response = "HTTP/1.1 404 Not Found\r\n" +
                                  "Content-Length: " + Encoding.UTF8.GetByteCount(errorHtmlContent) + "\r\n" +
                                  "Content-Type: text/html; charset=UTF-8\r\n" +
                                  "\r\n" +
                                  errorHtmlContent;

            }
            else
            {
                foreach (var film in movies)
                {
                    forma.Append("<div class=\"film\">");
                    forma.Append($"<h1>{film.Title} ({film.Release_date}) [{film.Rating}]</h1>");
                    forma.Append($"<p>{film.Description}</p>");
                    forma.Append($"<img src=\"https://image.tmdb.org/t/p/w500{film.Poster}\" ></img>");
                    forma.Append("</div>");
                }

                forma.Append("</div>");


                string htmlContent = File.ReadAllText(FilipPutanjaDoFajla) + forma.ToString() + @"
                    </body>
                    </html>";

                response =        "HTTP/2.0 200 OK\r\n" +
                                  "Access-Control-Allow-Origin:"+" http://127.0.0.1:5050" + "\r\n" +
                                  "Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS\r\n" +
                                  "Access-Control-Allow-Headers: Content-Type\r\n" +
                                  "Content-Length: " + Encoding.UTF8.GetByteCount(htmlContent) + "\r\n" +
                                  "Content-Type: text/html; charset=UTF-8\r\n" +
                                  "\r\n" +
                                  htmlContent;
            }
            return response; 
        }
    }
}
