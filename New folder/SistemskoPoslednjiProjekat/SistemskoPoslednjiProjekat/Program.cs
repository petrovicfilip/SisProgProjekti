using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemskoPoslednjiProjekat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var server = new SimpleHttpServer();
            await server.StartAsync();
        }
    }

    public class SimpleHttpServer
    {
        private readonly HttpListener _listener = new HttpListener();

        public SimpleHttpServer()
        {
            _listener.Prefixes.Add("http://localhost:5050/clanci/");
            _listener.Prefixes.Add("http://127.0.0.1:5050/clanci/");
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("Server started on http://localhost:5050/clanci/");
            while (true)
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => HandleRequestAsync(context));
            }
        }

        private async Task HandleRequestAsync(HttpListenerContext context)
        {
            var query = context.Request.QueryString["query"];
            if (string.IsNullOrEmpty(query))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errorBytes = Encoding.UTF8.GetBytes("Query parameter is missing.");
                await context.Response.OutputStream.WriteAsync(errorBytes, 0, errorBytes.Length);
                context.Response.Close();
                return;
            }

            var observer = new ClanakObserver("ArticleObserver");
            var clanakStream = new ClanakStream();

            clanakStream
                .Subscribe(observer);

            await clanakStream.GetArticlesAsync(query);

            var responseBytes = Encoding.UTF8.GetBytes("Fetching articles...");
            context.Response.ContentLength64 = responseBytes.Length;
            await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            context.Response.Close();
        }
    }
}