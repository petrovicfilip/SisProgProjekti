using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{
    public class MovieCacheItem
    {
        private List<string> _names;
        private string _url;
        public MovieCacheItem(List<string> names, string url)
        {
            _names = names;
            _url = url;
        }

        public List<string> Names { get { return _names; } }
        public string Url { get { return _url; } }
        public void SetUrl(string url) { _url = url; }
    }
    public static class MovieCache // LRU Cache !!!
    {
        private static readonly uint capacity = 1000; // za nase potrebe je dovoljno 1000, takodje koristimo benefit LRU-a bez bespotrebnog rasta kesa
                                                      //private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private static readonly Dictionary<string, LinkedListNode<MovieCacheItem>> cache = new Dictionary<string, LinkedListNode<MovieCacheItem>>();
        private static LinkedList<MovieCacheItem> _list = new LinkedList<MovieCacheItem>();
        private static readonly Object lockObject = new();
        public static List<string> readFromCache(string url)
        {
            //LinkedListNode<MovieCacheItem> node = null;
            try
            {
                if (cache.TryGetValue(url, out LinkedListNode<MovieCacheItem>? node))
                {
                    lock (lockObject)
                    {
                        _list.Remove(node); // O(n)
                        _list.AddLast(node);
                    }

                    return node.Value.Names;
                }
                else
                {
                    List<string> lista = new()
                    {
                        "~~"
                    };
                    return lista;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw; 
            }
        }

        public static void writeInCache(MovieCacheItem item)
        {
            lock (lockObject)
            {
                if (cache.TryGetValue(item.Url, out LinkedListNode<MovieCacheItem>? node))
                {
                    cache.Remove(item.Url);
                    _list.Remove(node); // O(n)
                }
                else if (cache.Count == capacity)
                {
                    string key = _list.First().Url;
                    _list.RemoveFirst();
                    cache.Remove(key);
                }
                LinkedListNode<MovieCacheItem> itemNode = new LinkedListNode<MovieCacheItem>(item);
                _list.AddLast(itemNode);
                cache[item.Url] = itemNode;
            }
        }

        public static void printCacheValues()
        {
            foreach (var item in cache)
            {
                Console.WriteLine("Za query :" + item.Key);
                foreach (var name in item.Value.Value.Names)
                {
                    Console.WriteLine(">" + name);
                }
            }
        }
    }
}
