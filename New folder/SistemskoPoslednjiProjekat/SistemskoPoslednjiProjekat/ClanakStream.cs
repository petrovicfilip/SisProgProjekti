using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SistemskoPoslednjiProjekat
{
    public class ClanakStream : IObservable<Clanak>
    {
        private readonly Subject<Clanak> clanakSubject = new Subject<Clanak>();
        private readonly ClanakService clanakService = new ClanakService();
        private readonly TopicModeler topicModeler;
       
        public async Task GetArticlesAsync(string query)
        {
            try
            {
                var articles = await clanakService.FetchClanciAsync(query);
                //TopicModeler tm = new TopicModeler("C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\New folder\\SistemskoPoslednjiProjekat\\SistemskoPoslednjiProjekat\\model.txt");

                foreach (var article in articles)
                {   
                    //article.Topic = tm.GetTopic(article.Content);
                    clanakSubject.OnNext(article);
                }
                clanakSubject.OnCompleted();
            }
            catch (Exception ex)
            {
                clanakSubject.OnError(ex);
            }
        }

        /*public IDisposable Subscribe(IObserver<Clanak> observer)
        {
            return _clanakSubject
                .SubscribeOn(TaskPoolScheduler.Default)
                .ObserveOn(NewThreadScheduler.Default)
                .Subscribe(observer);
        }*/

        // pogledati kada se prenosi Scheduler kao parametar
        //PROVERITI !!!
        public IDisposable Subscribe(IObserver<Clanak> observer)
        {
            return clanakSubject
                .SubscribeOn(TaskPoolScheduler.Default) // Postavi pozadinski task pool za izvrsavanje
                .ObserveOn(NewThreadScheduler.Default) // Posmatraj rezultate na novom thread-u
                .Where(clanak => !string.IsNullOrEmpty(clanak.Content)) // Filtriraj clanak sa nepraznim sadrzajem
                .Select(clanak =>
                {
                    clanak.Tilte = clanak.Tilte.ToUpper(); // Pretvori naslov clanka u velika slova
                    return clanak;
                })
                .Subscribe(observer);
        }
    }
}