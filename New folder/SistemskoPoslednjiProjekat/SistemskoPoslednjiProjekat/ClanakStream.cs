using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
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
            return _clanakSubject.Subscribe(observer);
        }
    }
}
