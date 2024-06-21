using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace SistemskoPoslednjiProjekat
{
    public class ClanakStream : IObservable<Clanak>
    {
        private readonly Subject<Clanak> clanakSubject = new Subject<Clanak>();
        private readonly ClanakService clanakService = new ClanakService();
        public async Task GetArticlesAsync(string query)
        {
            try
            {
                var articles = await clanakService.FetchClanciAsync(query);
                foreach (var article in articles)
                {
                    clanakSubject.OnNext(article);
                }
                clanakSubject.OnCompleted();
            }
            catch (Exception ex)
            {
                clanakSubject.OnError(ex);
            }
        }

        public IDisposable Subscribe(IObserver<Clanak> observer)
        {
            return clanakSubject.Subscribe(observer);
        }
    }
}
