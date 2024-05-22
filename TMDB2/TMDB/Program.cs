using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using TMDB;
using Python.Runtime;

class Program
{
    static int port = 5500;
    static TcpListener listener = new TcpListener(IPAddress.Any, port);
    static bool END = false;

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
        NetworkStream stream = tcpClient.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);
        /*
        using (NetworkStream stream = tcpClient.GetStream())
        using (StreamReader reader = new StreamReader(stream))
        using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
        */
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

               string response = Appearance.DrawPage(movies);

                foreach (var movie in movies)
                    Console.WriteLine(movie.Title);

               await writer.WriteAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}