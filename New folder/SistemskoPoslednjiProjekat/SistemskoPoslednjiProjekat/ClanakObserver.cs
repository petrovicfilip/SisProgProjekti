﻿using System;

namespace SistemskoPoslednjiProjekat
{
    public class ClanakObserver : IObserver<Clanak>
    {
        private readonly string name;

        public ClanakObserver(string name)
        {
            this.name = name;
        }

        public void OnNext(Clanak article)
        {
            Console.WriteLine($"Author: {article.Author}\nTitle: {article.Tilte}\n" +
                              $"Description: {article.Description}\nPublished: {article.publishedAt}\n" +
                              $"Content: {article.Content}\n");
        }

        public void OnError(Exception e)
        {
            Console.WriteLine($"Error occurred: {e.Message}\n");
        }

        public void OnCompleted()
        {
            Console.WriteLine("All articles have been successfully retrieved.");
        }
    }
}
