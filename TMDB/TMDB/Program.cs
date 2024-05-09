using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using TMDB;


/*/search - Text based search is the most common way. You provide a query string and we provide the closest match.
 * Searching by text takes into account all original, translated, alternative names and titles.
/discover - Sometimes it useful to search for movies and TV shows based on filters or definable values like ratings, 
certifications or release dates. The discover method make this easy.
/find - The last but still very useful way to find data is with existing external IDs. For example,
if you know the IMDB ID of a movie, TV show or person, you can plug that value into this method and we'll return anything that matches.
This can be very useful when you have an existing tool and are adding our service to the mix.*/
class Program
{


    static void Main(string[] args)
    {

        
       // mv.findMovieService("Jack Sparrow");

       List<string> list = new List<string>();
       list.Add("include_adult=false");

       List<string> lista = MovieService.searchMovieService("Titanic");
        foreach (var item in lista) 
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("..........................................");

        List<string> lista2 = MovieService.searchMovieService("Titanic");
        foreach (var item in lista2)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("..........................................");

        MovieCache.printCacheValues();

        //  LinkedListNode<int,int> = new LinkedListNode(new int[0]);   
        //string query = Console.ReadLine();
        //searchMovieService(query);
        //findMovieService(query);
    }

    
}