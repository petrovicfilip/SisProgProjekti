using System;
using System.Collections.Generic;
using SharpEntropy;
using SharpEntropy.IO;

namespace SistemskoPoslednjiProjekat
{
    public class TopicModeler
    {
        private readonly GisModel model;

        public TopicModeler(string modelFilePath)
        {
            var reader = new PlainTextGisModelReader("C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\New folder\\SistemskoPoslednjiProjekat\\SistemskoPoslednjiProjekat\\model.txt");
            model = new GisModel(reader);
        }

        public string GetTopic(string content)
        {
            var tokens = TokenizeContent(content);
            var context = tokens.ToArray();
            var bestOutcome = model.GetBestOutcome(model.Evaluate(context));
            return bestOutcome;
        }

        private List<string> TokenizeContent(string content)
        {
            return new List<string>(content.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
