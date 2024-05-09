using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Python.Runtime;
using TMDB;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static TcpListener listener = new TcpListener(IPAddress.Any, 5050);
    static HttpClient client = new HttpClient();
    static bool END = false;

    public static void Main(System.String[] args)
    {
        listener.Start();
        Console.WriteLine("Listening...");

        Thread acceptThread = new Thread(() => serveClients(listener));
        acceptThread.Start();

        Runtime.PythonDLL = @"C:\Users\BOBAN\AppData\Local\Programs\Python\Python311\python311.dll";

        PythonEngine.Initialize();

        using(Py.GIL())
        {
            dynamic sys = Py.Import("sys");
            sys.path.append("C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB\\TMDB");
            var pythonScript = Py.Import("klijenti.py");
            pythonScript.Invoke();
        }

        Console.WriteLine("Pokrenuta Nit");

        Console.WriteLine("Unesi EXIT za kraj programa");

        string? exit;
        do
            exit = Console.ReadLine();
        while (exit.ToUpper() != "EXIT");

        END = true;
        listener.Stop();

        acceptThread.Join();

        PythonEngine.Shutdown();

        return;
    }

    static private void serveClients(TcpListener listener)
    {
        while (!END)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(clientRequest, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    static void clientRequest(object? tcp_client)
    {
        TcpClient client = (TcpClient)tcp_client;
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);
            
        try
        {
            string input = reader.ReadLine();
            string[] parts = input.Split(' ');
            parts[1] = parts[1].Substring(1).Replace("%20", " ");
            string[] parametri = parts[1].Split("/");

            List<Movie> filmovi = new();
            switch (parametri[0]) 
            {
                case "search":
                    filmovi = MovieService.searchMovieService(parametri[1]);
                    break;
                case "find":
                    filmovi = MovieService.findMovieService(parametri[1]);
                    break;
                case "discover":
                    filmovi = MovieService.discoverMoviesService(parametri[1]);
                    break;
                default:
                    throw new Exception("Nije dobar zahtev ka Web Serveru!");
            }

            foreach (var film in filmovi)
                Console.WriteLine($"{film.Title} ({film.Release_date})  [{film.Rating}]\n {film.Description}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
