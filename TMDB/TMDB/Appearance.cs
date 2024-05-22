using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{
    internal class Appearance
    {
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

            string htmlContent = @"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <title>Lista Filmova</title>
                    <style>
                        body {
                             background-color: azure;
                             font-family: arial;
                        }
                        .film {
                            margin-bottom: 20px;
                            padding: 10px;
                            border: 10px solid #880000;
                            background-color: #ffcccc;
                            border-radius: 20px;
                        }
                        .film h1 {
                            margin-bottom: 5px;
                            color: #333;
                        }
                        .film p {
                            margin-top: 0;
                            color: #666;
                        }
                    </style>
                </head>
                <body >" + forma.ToString() + @"
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
