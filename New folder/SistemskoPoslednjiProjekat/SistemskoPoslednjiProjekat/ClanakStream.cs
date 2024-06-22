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
        private readonly Subject<Clanak> _clanakSubject = new Subject<Clanak>();
        private readonly ClanakService _clanakService = new ClanakService();

        public async Task GetArticlesAsync(string query)
        {
            try
            {
                var articles = await _clanakService.FetchClanciAsync(query);

                foreach (var article in articles)
                {
                    _clanakSubject.OnNext(article);
                }
                _clanakSubject.OnCompleted();
            }
            catch (Exception ex)
            {
                _clanakSubject.OnError(ex);
            }
        }

        public IDisposable Subscribe(IObserver<Clanak> observer)
        {
            return _clanakSubject
                .SubscribeOn(TaskPoolScheduler.Default)
                .ObserveOn(NewThreadScheduler.Default)
                .Subscribe(observer);
        }

        // pogledati kada se prenosi Scheduler kao parametar
        //PROVERITI !!!
        /*public IDisposable Subscribe(IObserver<Clanak> observer)
        {
            return _clanakSubject
                .SubscribeOn(TaskPoolScheduler.Default) // Postavi pozadinski task pool za izvrsavanje
                .ObserveOn(NewThreadScheduler.Default) // Posmatraj rezultate na novom thread-u
                .Where(clanak => !string.IsNullOrEmpty(clanak.Content)) // Filtriraj clanak sa nepraznim sadrzajem
                .Select(clanak =>
                {
                    clanak.Tilte = clanak.Tilte.ToUpper(); // Pretvori naslov clanka u velika slova
                    return clanak;
                })
                .Subscribe(observer);
        }*/
    }
}