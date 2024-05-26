using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using TMDB;
using Python.Runtime;
using System.Web.Http.Cors;
using System.Text;
using System.Collections;
using System.Text.Json;
using System.Net.Http;
class Program
{
    static int port = 5500;
    static TcpListener listener = new TcpListener(IPAddress.Any, port);
    static bool END = false;

 //   EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");

    public static async Task Main(string[] args)
    {
        listener.Start();
        Console.WriteLine("Listening on port " + port);

        var acceptTask = serveClientsAsync(listener);

        //PyTesting.Test();

        Console.WriteLine("Enter EXIT to end the program");

        string? exit;
        do
            exit = Console.ReadLine();
        while (exit.ToUpper() != "EXIT");

        END = true;
        listener.Stop();

        await acceptTask;

        //PythonEngine.Shutdown();

        return;
    }

    static async Task serveClientsAsync(TcpListener listener)
    {
        while (!END)
        {
            try
            {
                var tcpClient = await listener.AcceptTcpClientAsync();
                await Task.Run(() => HandleClientRequestAsync(tcpClient));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    static async Task HandleClientRequestAsync(TcpClient tcpClient)
    {
        /*      NetworkStream stream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);*/

        using (NetworkStream stream = tcpClient.GetStream())
        using (StreamReader reader = new StreamReader(stream))
        using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })

        {
            try
            {
                string input = await reader.ReadLineAsync();
                string[] parts = input.Split(' ');
                parts[1] = parts[1].Substring(1).Replace("%20", " ");
                string[] parameters = parts[1].Split("/");

                List<Movie> movies = new();
                switch (parameters[0])
                {
                    case "search":
                        movies = await MovieService.searchMovieServiceAsync(parameters[1]);
                        break;
                    case "find":
                        movies = await MovieService.findMovieServiceAsync(parameters[1]);
                        break;
                    case "discover":
                        movies = await MovieService.discoverMoviesServiceAsync(parameters[1]);
                        break;
                    default:
                        throw new Exception("Invalid request to Web Server!");
                }

               //string response = Appearance.DrawPage(movies);
                string response = JsonSerializer.Serialize(movies);
                foreach (var movie in movies)
                    Console.WriteLine(movie.Title);

                if (movies.Count > 0)
                    response = "HTTP/2.0 200 OK\r\n" +
                                  "Access-Control-Allow-Origin:" + " http://127.0.1.1:5050" + "\r\n" +
                                  "Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS\r\n" +
                                  "Access-Control-Allow-Headers: Content-Type\r\n" +
                                  "Content-Length: " + Encoding.UTF8.GetByteCount(response) + "\r\n" +
                                  "Content-Type: application/json; charset=UTF-8\r\n" +
                                  "\r\n" + response;
                else
                    response = File.ReadAllText("C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB2\\TMDB\\badresponse.txt");
                //await writer.WriteAsync(response);
                byte[] respb = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(respb, 0, respb.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}