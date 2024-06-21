using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskoPoslednjiProjekat
{
    public class ArticleObserver : IObserver<Clanak>
    {
        //private readonly string title;
        public ArticleObserver(string name)
        {
            //this.name = name;
        }

        public void OnNext(Clanak article)
        {
            Console.WriteLine($"Author: {article.Author}\n\n Title: {article.Tilte}\n\n" +
                              $"Desctription: {article.Description}\n\nObjavljeno: {article.publishedAt}\n\n" +
                              $"Content: {article.Content}\n\n");
        }

        public void OnError(Exception e)
        {
            Console.WriteLine($"Doslo je do greske!: {e.Message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Svi clanci su uspesno pribavljeni.");
        }
    }
}
